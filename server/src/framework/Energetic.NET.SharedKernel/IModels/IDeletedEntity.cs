using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel.IModels
{
    public interface IDeletedEntity
    {
        long? DeletedUserId { get; }

        string? DeletedBy { get; }

        DateTime? DeletedTime { get; }

        bool IsDeleted { get; }

        void Deleted(long deletedUserId, string deletedBy, DateTime deletedTime);

        void LogicDelete();

        void Recover();
    }
}
