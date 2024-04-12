using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("用户名密码登录")]
        UserName = 0,
        /// <summary>
        /// 手机号登录
        /// </summary>
        [Description("手机号登录")]
        PhoneNumber = 1,
        /// <summary>
        /// 邮箱登录
        /// </summary>
        [Description("邮箱登录")]
        EmailAddress = 2,
        /// <summary>
        /// 微信登录
        /// </summary>
        [Description("微信登录")]
        Wechat = 3,
    }
}
