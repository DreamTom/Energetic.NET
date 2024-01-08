using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.EmailService
{
    public interface IEmailAppService
    {
        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="toEmailAddress"></param>
        /// <param name="verificationOperationType">操作类型</param>
        /// <param name="length">验证码长度</param>
        /// <param name="expiry">验证码过期时间</param>
        /// <returns></returns>
        Task SendEmailVerificationCodeAsync(string toEmailAddress, VerificationOperationType verificationOperationType, int length = 4, int expiry = 120);
    }
}
