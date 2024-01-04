using Energetic.NET.Basic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.User.Dto
{
    public class UserDetailResponse
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string RealName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
    }
}
