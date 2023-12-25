using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel.IModels
{
    public interface ICreatedEntity
    {
        long CreatedUserId { get; }

        string CreatedBy { get; }

        DateTime CreatedTime { get; }

        void Created(long createUserId, string createdBy, DateTime createdTime);
    }
}
