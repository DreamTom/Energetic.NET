using Energetic.NET.SharedKernel.IModels;

namespace Energetic.NET.SharedKernel.BaseModels
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public long CreatedUserId { get; private set; }

        public string CreatedBy { get; private set; } = string.Empty;

        public DateTime CreatedTime { get; private set; }

        public long? UpdatedUserId { get; private set; }

        public string? UpdatedBy { get; private set; }

        public DateTime? UpdatedTime { get; private set; }

        public long? DeletedUserId { get; private set; }

        public string? DeletedBy { get; private set; }

        public DateTime? DeletedTime { get; private set; }

        public bool IsDeleted { get; set; }

        public void Created(long createUserId, string createdBy, DateTime createdTime)
        {
            CreatedUserId = createUserId;
            CreatedBy = createdBy;
            CreatedTime = createdTime;
        }

        public void Updated(long updateUserId, string updatedBy, DateTime updatedTime)
        {
            UpdatedUserId = updateUserId;
            UpdatedBy = updatedBy;
            UpdatedTime = updatedTime;
        }

        public void Deleted(long deletedUserId, string deletedBy, DateTime deletedTime)
        {
            DeletedUserId = deletedUserId;
            DeletedBy = deletedBy;
            DeletedTime = deletedTime;
        }

        public void Recover()
        {
            DeletedUserId = null;
            DeletedBy = null;
            DeletedTime = null;
        }
    }
}
