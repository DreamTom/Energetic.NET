using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel
{
    public class OrderByRequest
    {
        public required string PropName { get; set; }

        public bool IsDesc {  get; set; }
    }
}
