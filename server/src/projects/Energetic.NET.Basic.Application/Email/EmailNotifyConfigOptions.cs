using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Email
{
    /// <summary>
    /// 邮件通知配置
    /// </summary>
    public class EmailNotifyConfigOptions
    {
        public string FromEmailAddress { get; set; } = null!;

        public string? Link {  get; set; }

        public string? Company { get; set; }

        public string FromName { get; set; } = null!;
    }
}
