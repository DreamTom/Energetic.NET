namespace Energetic.NET.SharedKernel.BaseExceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base("请求资源未找到")
        {

        }
    }
}
