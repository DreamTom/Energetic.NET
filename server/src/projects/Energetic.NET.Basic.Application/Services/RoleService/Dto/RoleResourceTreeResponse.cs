using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Services.RoleService.Dto
{
    public record RoleResourceTreeResponse(long Id, long ParentId, string Name)
    {
        public string? Icon { get; set; }

        public string Title => Name;

        public ResourceType Type { get; set; }

        public List<RoleResourceTreeResponse>? Children { get; set; }
    }
}
