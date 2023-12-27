using Energetic.NET.SharedKernel.IModels;

namespace Energetic.NET.SharedKernel.BaseModels
{
    public abstract class BaseEntity : IEntity
    {
        public long Id { get; private set; }
    }
}
