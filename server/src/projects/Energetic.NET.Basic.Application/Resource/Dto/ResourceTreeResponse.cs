using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Resource.Dto
{
    public record ResourceTreeResponse(string Name, string RoutePath, int DisplayOrder)
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        public string? Icon { get; set; }

        public bool IsFolder { get; set; }

        public bool IsMenu { get; set; }

        public bool IsHide { get; set; }

        public List<ResourceTreeResponse> Children { get; set; } = [];
    }
}
