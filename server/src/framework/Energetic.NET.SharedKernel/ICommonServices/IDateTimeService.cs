using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
