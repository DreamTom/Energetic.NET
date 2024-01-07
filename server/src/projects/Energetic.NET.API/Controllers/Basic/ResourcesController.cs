using Energetic.NET.Basic.Application.Resource;
using Energetic.NET.Basic.Application.Resource.Dto;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IResponsitories;
using Energetic.NET.SharedKernel;
using Microsoft.AspNetCore.Authorization;

namespace Energetic.NET.API.Controllers.Basic
{
    /// <summary>
    /// 资源管理
    /// </summary>
    [AllowAnonymous]
    [UnitOfWork(typeof(BasicDbContext))]
    [Route("api/resources")]
    public class ResourcesController(IResourceDomainRepository resourceDomainRepository,
        BasicDbContext basicDbContext,
        IMapper mapper,
        IResourceAppService resourceAppService) : BaseController
    {

        /// <summary>
        /// 新增资源
        /// </summary>
        /// <param name="resourceAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResourceResponse>> Add(ResourceAddRequest resourceAdd)
        {
            var resource = new Resource(resourceAdd.Name, resourceAdd.DisplayOrder);
            if (resourceAdd.Type == ResourceType.Folder)
                resource.AddFolder(resourceAdd.RoutePath, resourceAdd.ReleationIds, resourceAdd.Icon, resourceAdd.IsEnable);
            else if (resourceAdd.Type == ResourceType.Menu)
            {
                if (await resourceDomainRepository.IsExistsPathAsync(resourceAdd.RoutePath))
                    return ValidateFailed("菜单路由已存在");
                resource.AddMenu(resourceAdd.RoutePath, resourceAdd.ReleationIds, resourceAdd.Icon, resourceAdd.IsEnable);
            }
            else
            {
                if (await resourceDomainRepository.IsExistsCodeAsync(resourceAdd.Code))
                    return ValidateFailed("按钮权限代码已存在");
                if (await resourceDomainRepository.IsExistsApiAsync(resourceAdd.ApiUrl, resourceAdd.RequestMethod.Value))
                    return ValidateFailed("按钮接口地址已存在");
                resource.AddButton(resourceAdd.ReleationIds, resourceAdd.Code, resourceAdd.ApiUrl, resourceAdd.RequestMethod.Value, resourceAdd.IsEnable);
            }
            await basicDbContext.AddAsync(resource);
            return Ok(mapper.Map<ResourceResponse>(resource));
        }

        /// <summary>
        /// 获取资源树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ResourceTreeResponse>>> GetResourceTree([FromQuery] ResourceQueryRequest resourceQuery)
        {
            return Ok(await resourceAppService.GetResourceTreeAsync(resourceQuery));
        }

        /// <summary>
        /// 获取菜单树，不包括按钮
        /// </summary>
        /// <returns></returns>
        [HttpGet("menuTree")]
        public async Task<ActionResult<TreeResponse>> GetMenuTree()
        {
            return Ok(await resourceAppService.GetMenuTreeAsync());
        }

        /// <summary>
        /// 修改资源
        /// </summary>
        /// <param name="resourceEdit"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ResourceResponse>> Edit(ResourceEditRequest resourceEdit)
        {
            var resource = await resourceDomainRepository.FindByIdAsync(resourceEdit.Id);
            if (resource == null)
                return DataNotFound("资源未找到或已被删除");
            resource = mapper.Map(resourceEdit, resource);
            basicDbContext.Update(resource);
            return Ok(resource);
        }

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<long>> Delete(long id)
        {
            var resource = await resourceDomainRepository.FindByIdAsync(id);
            if (resource == null)
                return DataNotFound("资源未找到或已被删除");
            resource.IsDeleted = true;
            basicDbContext.Update(resource);
            return Ok(id);
        }
    }
}
