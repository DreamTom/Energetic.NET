using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Domain.Models
{
    public class RoleResource : BaseCreatedEntity
    {
        public long RoleId { get; set; }

        public long ResourceId { get; set; }
    }
}
