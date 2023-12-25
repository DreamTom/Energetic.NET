using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel.IModels
{
    public interface IUpdatedEntity
    {
        long? UpdatedUserId { get; }

        string? UpdatedBy { get; }

        DateTime? UpdatedTime { get; }

        void Updated(long updateUserId, string updatedBy, DateTime updatedTime);
    }
}
