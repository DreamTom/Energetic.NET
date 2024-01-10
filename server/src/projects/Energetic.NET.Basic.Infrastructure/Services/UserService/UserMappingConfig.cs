using Energetic.NET.Basic.Application.UserService.Dto;
using Mapster;

namespace Energetic.NET.Basic.Infrastructure.UserService
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserResponse>();
            config.NewConfig<Resource, UserResourceTreeResponse>().MapToConstructor(true)
                .Map(dest => dest.TempId , src=> src.Id);
        }
    }
}
