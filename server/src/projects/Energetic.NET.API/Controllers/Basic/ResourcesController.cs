using Energetic.NET.Basic.Application.Resource;
using Energetic.NET.Basic.Application.Resource.Dto;
using Energetic.NET.Basic.Domain.IResponsitories;
using Microsoft.AspNetCore.Authorization;

namespace Energetic.NET.API.Controllers.Basic
{
    /// <summary>
    /// 资源管理
    /// </summary>
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResourceResponse>> Add(ResourceAddRequest resourceAdd)
        {
            var resource = new Resource(resourceAdd.Name, resourceAdd.RoutePath, resourceAdd.DisplayOrder);
            if (resourceAdd.IsFolder)
                resource.AddFolder(resourceAdd.IsHide, resourceAdd.ParentId);
            if (resourceAdd.IsMenu)
            {
                if (await resourceDomainRepository.IsExistsPathAsync(resourceAdd.RoutePath))
                    return ValidateFailed("菜单路由已存在");
                resource.AddMenu(resourceAdd.IsHide, resourceAdd.Icon, resourceAdd.ParentId);
            }
            if (!resourceAdd.IsFolder && !resourceAdd.IsMenu)
            {
                if (await resourceDomainRepository.IsExistsCodeAsync(resourceAdd.Code))
                    return ValidateFailed("按钮权限代码已存在");
                if (await resourceDomainRepository.IsExistsApiAsync(resourceAdd.ApiUrl, resourceAdd.RequestMethod.Value))
                    return ValidateFailed("按钮接口地址已存在");
                resource.AddButton(resourceAdd.IsHide, resourceAdd.ParentId, resourceAdd.Code, resourceAdd.ApiUrl, resourceAdd.RequestMethod.Value);
            }
            await basicDbContext.AddAsync(resource);
            return Ok(mapper.Map<ResourceResponse>(resource));
        }

        /// <summary>
        /// 获取资源树
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ResourceTreeResponse>>> GetResourceTree()
        {
            return Ok(await resourceAppService.GetResourceTreeAsync());
        }
    }
}
