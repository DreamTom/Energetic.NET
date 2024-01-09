using Energetic.NET.Basic.Application.ResourceService.Dto;
using Energetic.NET.Basic.Application.RoleService;
using Energetic.NET.Basic.Application.RoleService.Dto;
using Energetic.NET.Basic.Application.Services.RoleService.Dto;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.Basic.Domain.Services;
using Microsoft.AspNetCore.Authorization;

namespace Energetic.NET.API.Controllers.Basic
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [AllowAnonymous]
    [UnitOfWork(typeof(BasicDbContext))]
    [Route("api/roles")]
    public class RolesController(IRoleAppService roleAppService,
        IMapper mapper,
        IRoleDomainRepository roleDomainRepository,
        RoleDomainService roleDomainService) : BaseController
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="roleQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<RoleResponse>>> GetPageList([FromQuery] RoleQueryRequest roleQuery)
        {
            return Ok(await roleAppService.GetPageListAsync(roleQuery));
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="roleAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<RoleResponse>> Add(RoleEditRequest roleAdd)
        {
            if (await roleDomainRepository.IsExistsCodeAsync(roleAdd.Code))
                return ValidateFailed("角色标识已存在");
            var role = mapper.Map<Role>(roleAdd);
            await roleDomainRepository.AddAsync(role);
            return Ok(mapper.Map<RoleResponse>(role));
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleEdit"></param>
        /// <returns></returns>
        [HttpPut("{id:long}")]
        public async Task<ActionResult<RoleResponse>> Edit(long id, RoleEditRequest roleEdit)
        {
            var role = await roleDomainRepository.FindByIdAsync(id);
            if (role == null)
                return ValidateFailed("角色不存在或已被删除");
            if (await roleDomainRepository.IsExistsCodeAsync(roleEdit.Code, id))
                return ValidateFailed("角色标识已存在");
            role = mapper.Map(roleEdit, role);
            roleDomainRepository.Update(role);
            return Ok(mapper.Map<RoleResponse>(role));
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<long>> Delete(long id)
        {
            var role = await roleDomainRepository.FindByIdAsync(id);
            if (role == null)
                return ValidateFailed("角色不存在或已被删除");
            roleDomainRepository.LogicDelete(role);
            return Ok(id);
        }

        /// <summary>
        /// 获取角色资源权限ID集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}/resourceIds")]
        public async Task<ActionResult<long[]>> GetRoleResourceIds(long id)
        {
            if (!await roleDomainRepository.IsExistsIdAsync(id))
                return DataNotFound("角色不存在或已被删除");
            var role = await roleDomainRepository.GetRoleIncludeResourcesAsync(id);
            return Ok(role.Resources.Select(r => r.Id).ToArray());
        }

        /// <summary>
        /// 更新角色资源权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        [HttpPut("{id:long}/resources")]
        public async Task<ActionResult<List<ResourceResponse>>> EditRoleResources(long id, long[] resourceIds)
        {
            if (!await roleDomainRepository.IsExistsIdAsync(id))
                return DataNotFound("角色不存在或已被删除");
            var role = await roleDomainService.UpdateRoleResourcesAsync(id, resourceIds);
            roleDomainRepository.Update(role);
            return Ok(mapper.Map<List<ResourceResponse>>(role.Resources));
        }
    }
}
