using Energetic.NET.SharedKernel.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel
{
    public interface ICurrentUserService
    {
        IUserInfo? CurrentUser { get; }

        IUserInfo? GetCurrentUserInfo();

        string GetClientIpAddress();
    }
}
