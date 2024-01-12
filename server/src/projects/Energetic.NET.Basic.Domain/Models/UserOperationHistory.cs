using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Domain.Models
{
    public class UserOperationHistory(string moduleName, string functionName, string requestAddress,
        RequestMethod requestMethod, ResultType requestResult, int takingTime, string? message = default) : BaseEntity, IAggregateRoot
    {
        public long? UserId { get; private set; }

        public string? OperatorAccount { get; private set; }

        public string? NickName { get; private set; }

        public string ModuleName { get; init; } = moduleName;

        public string FunctionName { get; init; } = functionName;

        public string RequestAddress { get; init; } = requestAddress;

        public RequestMethod RequestMethod { get; init; } = requestMethod;

        public ResultType RequestResult { get; init; } = requestResult;

        public int TakingTime { get; init; } = takingTime;

        public DateTime CreatedTime { get; init; } = DateTime.Now;

        public string? Message { get; init; } = message;

        public void SetUserInfo(long userId, string opearationAccount, string nickName)
        {
            UserId = userId;
            OperatorAccount = opearationAccount;
            NickName = nickName;
        }
    }
}
