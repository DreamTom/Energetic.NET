using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.User.Dto
{
    public class LoginResponse
    {
        public UserDetailResponse UserInfo { get; set; }

        public string Token { get; set; }
    }
}
