namespace Energetic.NET.SharedKernel.BaseExceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public BaseException(int errorCode, string message) : this(message)
        {
            ErrorCode = errorCode;
        }

        public BaseException(Enum enumValue)
        {
            ErrorCode = enumValue.ToInt();
            ErrorMessage = enumValue.GetDescription();
        }

        public int ErrorCode { get; }

        public string ErrorMessage { get; }
    }
}
