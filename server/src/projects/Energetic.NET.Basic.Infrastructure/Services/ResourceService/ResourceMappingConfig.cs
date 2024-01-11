using Energetic.NET.Basic.Application.ResourceService.Dto;
using Mapster;

namespace Energetic.NET.Basic.Infrastructure.ResourceService
{
    public class ResourceMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Resource, ResourceResponse>().MapToConstructor(true);
            config.NewConfig<Resource, ResourceTreeResponse>().MapToConstructor(true);
            config.NewConfig<ResourceEditRequest, Resource>().MapToConstructor(true)
                .Map(dest => dest.ParentId, src => GetParentId(src.ReleationIds));
            config.NewConfig<Resource, SimpleResourceTreeResponse>().MapToConstructor(true);
        }

        private static long GetParentId(string releationIds)
        {
            long parentId = 0;
            if (!string.IsNullOrWhiteSpace(releationIds))
            {
                var lastId = releationIds.Split('/').Last();
                if (long.TryParse(lastId, out long id))
                    parentId = id;
            }
            return parentId;
        }
    }
}
