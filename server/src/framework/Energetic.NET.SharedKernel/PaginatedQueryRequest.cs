using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel
{
    public record PaginatedQueryRequest(int PageNumber, int PageSize)
    {
        public List<OrderByRequest> OrderBy { get; set; } = [];
    }
}
