using Energetic.NET.Basic.Application.RoleService.Dto;
using Energetic.NET.SharedKernel;
using Mapster;

namespace Energetic.NET.Basic.Infrastructure.Services.RoleService
{
    public class RoleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Role, RoleResponse>().MapToConstructor(true);
            config.NewConfig<RoleEditRequest, Role>().MapToConstructor(true);
            config.NewConfig<Resource, RoleResourceTreeResponse>().MapToConstructor(true);
            config.NewConfig<Role, DropdownItemResponse>()
                .Map(desc => desc.Label, src => src.Name)
                .Map(desc => desc.Value, src => src.Id.ToString());
        }
    }
}
