using Energetic.NET.Basic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Resource.Dto
{
    public record ResourceTreeResponse(string Name, int DisplayOrder)
    {
        public long Id { get; set; }

        public string? RoutePath { get; set; }

        public long ParentId { get; set; }

        public string ReleationIds { get; set; } = string.Empty;

        public string? Icon { get; set; }

        public ResourceType Type { get; set; }

        public bool IsEnable { get; set; }

        public string? ApiUrl { get; set; }

        public string? Code { get; set; }

        public RequestMethod? RequestMethod { get; set; }

        public List<ResourceTreeResponse>? Children { get; set; }
    }
}
