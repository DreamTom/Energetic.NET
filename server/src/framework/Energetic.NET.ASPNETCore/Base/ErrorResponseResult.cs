using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.ASPNETCore
{
    /// <summary>
    /// 错误响应消息内容
    /// </summary>
    public class ErrorResponseResult
    {
        /// <summary>
        /// 错误响应消息内容
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误消息</param>
        public ErrorResponseResult(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        /// <summary>
        /// 错误响应消息内容
        /// </summary>
        /// <param name="enumValue">错误枚举值</param>
        public ErrorResponseResult(Enum enumValue)
        {
            ErrorCode = enumValue.ToInt();
            Message = enumValue.GetDescription();
        }

        /// <summary>
        /// 错误响应消息内容：状态码为0，前端直接返回后端错误消息
        /// </summary>
        /// <param name="message">错误消息</param>
        public ErrorResponseResult(string message)
        {
            Message = message;
        }

        public int ErrorCode { get; }

        public string Message { get; }
    }
}
