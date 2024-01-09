using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Domain.Models
{
    public class RoleResource : ICreatedEntity
    {
        public long RoleId { get; set; }

        public long ResourceId { get; set; }

        public long CreatedUserId { get; private set; }

        public string CreatedBy { get; private set; } = string.Empty;

        public DateTime CreatedTime { get; private set; }

        public void Created(long createUserId, string createdBy, DateTime createdTime)
        {
            CreatedUserId = createUserId;
            CreatedBy = createdBy;
            CreatedTime = createdTime;
        }
    }
}
