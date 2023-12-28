using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Infrastructure
{
    public class PaginatedQueryRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
