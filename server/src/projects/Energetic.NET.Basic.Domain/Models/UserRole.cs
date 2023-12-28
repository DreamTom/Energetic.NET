using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Domain.Models
{
    public class UserRole : BaseCreatedEntity
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}
