using System.Linq;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static string ToBase64String(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string? FromBase64String(this string base64, bool throwError = false)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return base64;

            if (throwError)
            {
                byte[] bytes = Convert.FromBase64String(base64);
                return Encoding.UTF8.GetString(bytes);
            }
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return null;
            }
        }

        public static string ToPascal(this string convertText)
        {
            if (!convertText.Any(x => x >= 'a' && x <= 'z'))
                return convertText;
            var firstLetter = convertText.First();
            if (firstLetter >= 'a' && firstLetter <= 'z')
                return string.Concat(firstLetter.ToString().ToUpper(), convertText.AsSpan(1));
            return convertText;
        }

        /// <summary>
        /// 转换为小写下划线分隔命名
        /// </summary>
        /// <param name="convertText"></param>
        /// <returns></returns>
        public static string ToSnakeCase(this string convertText)
        {
            if (!convertText.Any(x => x >= 'A' && x <= 'Z'))
                return convertText;
            string newName = string.Empty;
            if (convertText.Contains('_'))
            {
                var newArr = convertText.Split('_').Select(c => c.ToLower());
                return string.Join("_", newArr);
            }
            for (var i = 0; i < convertText.Length; i++)
            {
                var letter = convertText[i];
                if (letter >= 'A' && letter <= 'Z')
                {
                    string tempString = letter.ToString().ToLower();
                    if (i != 0)
                        tempString = "_" + tempString;
                    newName += tempString;
                }
                else
                {
                    newName += letter.ToString();
                }
            }
            return newName;
        }

        /// <summary>
        /// 转换为小驼峰命名
        /// </summary>
        /// <param name="convertText"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string convertText)
        {
            if (!convertText.Any(x => x >= 'A' && x <= 'Z'))
                return convertText;
            var firstLetter = convertText.First();
            if (firstLetter >= 'A' && firstLetter <= 'Z')
                return string.Concat(firstLetter.ToString().ToLower(), convertText.AsSpan(1));
            return convertText;
        }

        /// <summary>
        /// 转换为小写中线分隔命名
        /// </summary>
        /// <param name="convertText"></param>
        /// <returns></returns>
        public static string ToKebabCase(this string convertText)
        {
            string newName = string.Empty;
            for (var i = 0; i < convertText.Length; i++)
            {
                var letter = convertText[i];
                if (letter >= 'A' && letter <= 'Z')
                {
                    string tempString = letter.ToString();
                    if (i != 0)
                        tempString = "-" + tempString;
                    newName += tempString;
                }
                else
                {
                    newName += letter.ToString();
                }
            }
            return newName;
        }

        public static bool EqualsIgnoreCase(this string s1, string s2)
        {
            return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 截取字符串s1最多前maxLen个字符
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="maxLen"></param>
        /// <returns></returns>
        public static string Cut(this string s1, int maxLen)
        {
            if (s1 == null)
            {
                return string.Empty;
            }
            int len = s1.Length <= maxLen ? s1.Length : maxLen;//不能超过字符串的最大大小
            return s1[0..len];
        }
    }
}
