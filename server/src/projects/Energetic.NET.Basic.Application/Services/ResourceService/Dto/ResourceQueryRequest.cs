using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.ResourceService.Dto
{
    public class ResourceQueryRequest
    {
        public string? Name { get; set; }

        public string? RoutePath { get; set; }

        public string? Code { get; set; }

        public bool? IsEnable { get; set; }
    }
}
