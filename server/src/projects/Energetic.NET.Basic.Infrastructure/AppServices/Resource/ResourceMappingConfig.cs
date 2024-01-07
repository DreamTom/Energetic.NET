using Energetic.NET.Basic.Application.Resource.Dto;
using Energetic.NET.SharedKernel;
using Mapster;

namespace Energetic.NET.Basic.Infrastructure.AppServices.Resource
{
    public class ResourceMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Models.Resource, ResourceResponse>().MapToConstructor(true);
            config.NewConfig<Domain.Models.Resource, ResourceTreeResponse>().MapToConstructor(true);
            config.NewConfig<ResourceEditRequest, Domain.Models.Resource>().MapToConstructor(true);
            config.NewConfig<Domain.Models.Resource, TreeResponse>().Map(dest => dest.Label, src => src.Name);
        }
    }
}
