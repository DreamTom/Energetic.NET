using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.User.Dto
{
    public class LoginResponse
    {
        public string UserName { get; set; }

        public string NickName { get; set; }

        public string RealName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }
    }
}
