using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.RoleService.Dto
{
    public record RoleQueryRequest(int PageNumber, int PageSize) : PaginatedQueryRequest(PageNumber, PageSize)
    {
        public string? Name { get; set; }

        public string? Code { get; set; }
    }
}
