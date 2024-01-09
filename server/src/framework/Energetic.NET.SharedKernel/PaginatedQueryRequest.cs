using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel
{
    public record PaginatedQueryRequest(int PageNumber, int PageSize)
    {
        public string? PropName { get; set; }

        public string? OrderBy { get; set; }
    }
}
