using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel
{
    public record PaginatedQueryRequest(int PageNumber = 1, int PageSize = 20)
    {
        public string? PropName { get; set; }

        public string? OrderBy { get; set; }

        public PaginatedQueryRequest GetPaginatedQuery()
        {
            return new PaginatedQueryRequest(PageNumber, PageSize)
            {
                PropName = PropName,
                OrderBy = OrderBy
            };
        }
    }
}
