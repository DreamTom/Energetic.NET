using Energetic.NET.SharedKernel;

namespace Energetic.NET.Infrastructure
{
    internal class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
