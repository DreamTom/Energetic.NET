using Energetic.NET.Basic.Application.Resource.Dto;
using Mapster;

namespace Energetic.NET.Basic.Infrastructure.AppServices.Resource
{
    public class ResourceMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Models.Resource, ResourceResponse>().MapToConstructor(true);
            config.NewConfig<Domain.Models.Resource, ResourceTreeResponse>().MapToConstructor(true);
        }
    }
}
