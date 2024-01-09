using Energetic.NET.SharedKernel.IModels;

namespace Energetic.NET.SharedKernel.BaseModels
{
    public abstract class BaseCreatedEntity : ICreatedEntity
    {
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
