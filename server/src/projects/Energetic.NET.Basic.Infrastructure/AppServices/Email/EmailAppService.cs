using EasyCaching.Core;
using Energetic.NET.Basic.Application;
using Energetic.NET.Basic.Application.Email;
using Energetic.NET.Basic.Domain.Constants;
using Energetic.NET.Common.Helpers;
using Energetic.NET.SharedKernel.ICommonServices;
using MailKitSimplified.Sender.Abstractions;
using Microsoft.Extensions.Options;

namespace Energetic.NET.Basic.Infrastructure.AppServices.Email
{
    internal class EmailAppService(IEmailWriter emailWriter,
        IRazorTemplateEngine razorTemplateEngine,
        IOptionsSnapshot<EmailNotifyConfigOptions> emailNotifyOptions,
        IEasyCachingProvider cachingProvider) : IEmailAppService
    {
        public async Task SendEmailVerificationCodeAsync(string toEmailAddress,
            VerificationOperationType verificationOperationType, int length = 4, int expiry = 120)
        {
            var rndNumber = RandomHelper.GenNumberCode(length);
            await cachingProvider.SetAsync(GetEmailNotifyCachingKey(verificationOperationType, toEmailAddress),
                rndNumber, TimeSpan.FromSeconds(expiry));
            var emailNotifyConfig = emailNotifyOptions.Value;
            var templatePath = Path.Combine("Templates", "email-notify-template.html");
            var body = await razorTemplateEngine.RenderAsync(templatePath, new EmailNotifyTemplate()
            {
                Company = emailNotifyConfig.Company,
                Link = emailNotifyConfig.Link,
                VerificationCode = rndNumber,
                VerificationOperationType = verificationOperationType,
            });
            await emailWriter.From(emailNotifyConfig.FromName, emailNotifyConfig.FromEmailAddress)
                .To(toEmailAddress)
                .Subject($"正在进行{verificationOperationType.GetDescription()}操作")
                .BodyHtml(body)
                .SendAsync();
        }

        private static string GetEmailNotifyCachingKey(VerificationOperationType verificationOperationType, string emailAddress)
        {
            string key = string.Empty;
            switch (verificationOperationType)
            {
                case VerificationOperationType.LoginOrRegister:
                    key = CacheKey.LoginByEmailAddressPrefix + emailAddress;
                    break;
                case VerificationOperationType.ChangeEmailAddress:
                    key = CacheKey.ChangeEmailAddressPrefix + emailAddress;
                    break;
                default:
                    break;
            }
            return key;
        }
    }
}
