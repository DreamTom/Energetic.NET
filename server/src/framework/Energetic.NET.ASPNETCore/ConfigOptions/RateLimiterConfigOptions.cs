namespace Energetic.NET.ASPNETCore.ConfigOptions
{
    public class RateLimiterConfigOptions
    {
        public required FixedWindowOptions FixedWindow { get; set; }
    }

    /// <summary>
    /// 固定时间窗配置
    /// </summary>
    public class FixedWindowOptions
    {
        /// <summary>
        /// 固定时间窗口请求限制
        /// </summary>
        public int PermitLimit { get; set; }

        /// <summary>
        /// 时间窗口
        /// </summary>
        public int PermitSeconds {  get; set; }

        /// <summary>
        /// 队列限制请求数
        /// </summary>
        public int QueueLimit { get; set; }
    }
}
