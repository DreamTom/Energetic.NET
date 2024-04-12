using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Domain.Models
{
    public class UserLoginHistory(long? userId, string loginAccount, LoginWay loginWay,
        ResultType loginResult, string loginIp, string? message = default)
        : BaseEntity, IAggregateRoot
    {
        public long? UserId { get; init; } = userId;

        public string LoginAccount { get; init; } = loginAccount;

        public string LoginIp { get; init; } = loginIp;

        public string LoginLocation { get; private set; } = string.Empty;

        public string Browser { get; private set; } = string.Empty;

        public string OperatingSystem { get; private set; } = string.Empty;

        public LoginWay LoginWay { get; init; } = loginWay;

        public ResultType LoginResult { get; init; } = loginResult;

        public string? Message { get; init; } = message;

        public DateTime CreatedTime { get; init; } = DateTime.Now;

        public void SetClientInfo(string loginLocation, string browser, string operatingSystem)
        {
            LoginLocation = loginLocation;
            Browser = browser;
            OperatingSystem = operatingSystem;
        }
    }
}
