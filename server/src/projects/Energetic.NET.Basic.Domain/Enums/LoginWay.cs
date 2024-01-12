using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Domain.Enums
{
    public enum LoginWay
    {
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        UserName = 0,
        /// <summary>
        /// 手机号登录
        /// </summary>
        PhoneNumber = 1,
        /// <summary>
        /// 邮箱登录
        /// </summary>
        EmailAddress = 2,
        /// <summary>
        /// 微信登录
        /// </summary>
        Wechat = 3,
    }
}
