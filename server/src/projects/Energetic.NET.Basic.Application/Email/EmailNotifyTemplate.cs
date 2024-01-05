using Energetic.NET.Basic.Application;
using RazorEngineCore;

namespace Energetic.NET.Basic.Application.Email
{
    /// <summary>
    /// 邮件通知模板
    /// </summary>
    public class EmailNotifyTemplate : RazorEngineTemplateBase
    {
        public string? Link { get; init; }

        public string? Company { get; init; }

        public string OperationType => VerificationOperationType.GetDescription();

        public VerificationOperationType VerificationOperationType { get; init; }

        public string VerificationCode { get; init; } = null!;
    }
}
