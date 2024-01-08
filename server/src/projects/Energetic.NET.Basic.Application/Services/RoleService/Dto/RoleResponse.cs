using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.RoleService.Dto
{
    public record RoleResponse(string Name, string Code, string CreatedBy, DateTime CreatedTime)
    {
        public long Id { get; set; }

        public string? Description { get; set; }
    }
}
