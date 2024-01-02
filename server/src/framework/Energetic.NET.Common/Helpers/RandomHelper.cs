using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Common.Helpers
{
    public static class RandomHelper
    {
        private static readonly string s_letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 生成随机字母验证码
        /// </summary>
        /// <param name="num">验证码位数</param>
        /// <returns></returns>
        public static string GenLetterCode(int num)
        {
            string code = string.Empty;
            for (int i = 0; i < num; i++)
            {
                var index = Random.Shared.Next(s_letters.Length);
                var letter = s_letters[index];
                code += letter;
            }
            return code;
        }
    }
}
