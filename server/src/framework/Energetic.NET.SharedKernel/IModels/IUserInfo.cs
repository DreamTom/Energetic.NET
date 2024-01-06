using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel.IModels
{
    public interface IUserInfo
    {
        long Id { get; }

        long TenantId { get; }

        int JwtVersion { get; }

        string? UserName { get; }

        string NickName { get; }

        string? RealName { get; }
    }
}
