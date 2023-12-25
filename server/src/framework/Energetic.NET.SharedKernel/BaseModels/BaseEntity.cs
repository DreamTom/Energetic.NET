using Energetic.NET.SharedKernel.IModels;

namespace Energetic.NET.SharedKernel.BaseModels
{
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            Id = 1;
        }

        public long Id { get; }
    }
}
