using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.RoleService.Dto
{
    public record RoleResourceTreeResponse(long Id, long ParentId, string Name)
    {
        public string? Icon { get; set; }

        public string Title => Name;

        public ResourceType Type { get; set; }

        public List<RoleResourceTreeResponse>? Children { get; set; }
    }
}
