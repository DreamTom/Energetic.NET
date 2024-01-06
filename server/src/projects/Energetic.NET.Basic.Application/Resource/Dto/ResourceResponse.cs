using Energetic.NET.Basic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Resource.Dto
{
    public record ResourceResponse(string Name, string RoutePath, int DisplayOrder)
    {
        public long Id { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public string? Code { get; set; }

        public long ParentId { get; set; }

        public string? Icon { get; set; }

        public bool IsFolder { get; set; }

        public bool IsMenu { get; set; }

        public bool IsHide { get; set; }

        public string? ApiUrl { get; set; }

        public RequestMethod? RequestMethod { get; set; }
    }
}
