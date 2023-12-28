using System.ComponentModel;

namespace Energetic.NET.Basic.Domain.Enums
{
    public enum NotifyType
    {
        [Description("登录")]
        Login = 1,

        [Description("注册")]
        Register,

        [Description("修改邮箱")]
        ChangeEmailAddress,

        [Description("修改手机号")]
        ChangePhoneNumber,

        [Description("重置密码")]
        ResetPassword,
    }
}
