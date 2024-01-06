using Energetic.NET.Basic.Application.User.Dto;
using Mapster;

namespace Energetic.NET.Basic.Infrastructure.AppServices.User
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Models.User, UserDetailResponse>()
                  .Map(dest => dest.UserId, src => src.Id);
        }
    }
}
