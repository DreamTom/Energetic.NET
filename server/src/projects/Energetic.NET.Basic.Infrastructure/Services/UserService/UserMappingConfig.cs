using Energetic.NET.Basic.Application.UserService.Dto;
using Mapster;

namespace Energetic.NET.Basic.Infrastructure.UserService
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserDetailResponse>()
                  .Map(dest => dest.UserId, src => src.Id);
        }
    }
}
