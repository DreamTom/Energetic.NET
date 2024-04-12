using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Services.UserService.Dto
{
    public class UserLoginHistoryResponse
    {
        public long? UserId { get; set; }

        public string LoginAccount { get; set; } = string.Empty;

        public string LoginIp { get; set; } = string.Empty;

        public string LoginLocation { get; set; } = string.Empty;

        public string Browser { get; set; } = string.Empty;

        public string OperatingSystem { get; set; } = string.Empty;

        public LoginWay LoginWay { get; set; }

        public string LoginWayDesc => LoginWay.GetDescription();

        public ResultType LoginResult { get; set; }

        public string ResultTypeDesc => LoginResult.GetDescription();

        public string? Message { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
