using System.ComponentModel;

namespace Energetic.NET.Basic.Domain.Enums
{
    public enum LoginFailedResult
    {
        [Description("用户名或密码错误")]
        UserNameOrPasswordIsWrong,

        [Description("用户被禁用")]
        UserIsForbidden,

        [Description("短信验证码错误")]
        SmsVerificationCodeIsWrong,

        [Description("邮箱验证码错误")]
        EmailVerifcationCodeIsWrong
    }
}
