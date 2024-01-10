namespace Energetic.NET.ASPNETCore.Security
{
    public struct RateLimiterPolicy
    {
        /// <summary>
        /// 固定窗口
        /// </summary>
        public const string Fixed = "fixed";

        /// <summary>
        /// 滑动窗口
        /// </summary>
        public const string Sliding = "sliding";
    }
}
