namespace Energetic.NET.SharedKernel.BaseExceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException() : base("请求验证失败")
        {
        }
    }
}
