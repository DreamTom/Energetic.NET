using Energetic.NET.Basic.Application.RoleService.Dto;
using Mapster;

namespace Energetic.NET.Basic.Infrastructure.Services.RoleService
{
    public class RoleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Role, RoleResponse>().MapToConstructor(true);
            config.NewConfig<RoleEditRequest, Role>().MapToConstructor(true);
        }
    }
}
