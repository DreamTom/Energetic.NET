using System.ComponentModel;

namespace Energetic.NET.Basic.Application
{
    /// <summary>
    /// 验证操作类型
    /// </summary>
    public enum VerificationOperationType
    {
        [Description("登录/注册")]
        LoginOrRegister,

        [Description("修改邮箱地址")]
        ChangeEmailAddress,

        [Description("修改手机号")]
        ChangePhoneNumber,

        [Description("重置密码")]
        ResetPassword,
    }
}
