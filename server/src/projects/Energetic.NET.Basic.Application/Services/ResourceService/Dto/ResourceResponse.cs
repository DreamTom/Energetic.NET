using Energetic.NET.Basic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.ResourceService.Dto
{
    public record ResourceResponse(string Name, int DisplayOrder)
    {
        public long Id { get; set; }

        public string? RoutePath { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public string? Code { get; set; }

        public string ReleationIds { get; set; } = string.Empty;

        public string? Icon { get; set; }

        public ResourceType Type { get; set; }

        public bool IsEnable { get; set; }

        public string? ApiUrl { get; set; }

        public RequestMethod? RequestMethod { get; set; }
    }
}
