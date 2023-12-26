using System.Security.Cryptography;
using System.Text;

namespace Energetic.NET.Common
{
    /// <summary>
    /// Hash工具类
    /// </summary>
    public static class HashHelper
    {
        private static string HashString(HashAlgorithm hashAlgorithm, string text, bool toLower, Encoding? encoding)
        {
            ArgumentNullException.ThrowIfNull(hashAlgorithm);

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            encoding ??= Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(text);
            byte[] buffer = hashAlgorithm.ComputeHash(bytes);
            var res = buffer.ToHexString();
            return toLower ? res.ToLower() : res;
        }

        private static string HashSteam(HashAlgorithm hashAlgorithm, Stream stream, bool toLower)
        {
            byte[] buffer = hashAlgorithm.ComputeHash(stream);
            var res = buffer.ToHexString();
            return toLower ? res.ToLower() : res;
        }

        /// <summary>
        /// 字符串的SHA1签名
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toLower"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ComputeSha1Hash(this string text, bool toLower = true, Encoding? encoding = null)
        {
            using var sha1 = SHA1.Create();
            return HashString(sha1, text, toLower, encoding);
        }

        /// <summary>
        /// 字符串的SHA256签名
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toLower"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ComputeSha256Hash(this string text, bool toLower = true, Encoding? encoding = null)
        {
            using var sha256 = SHA256.Create();
            return HashString(sha256, text, toLower, encoding);
        }

        /// <summary>
        /// 字符串SHA512的签名
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toLower"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ComputeSha512Hash(this string text, bool toLower = true, Encoding? encoding = null)
        {
            using var sha512 = SHA512.Create();
            return HashString(sha512, text, toLower, encoding);
        }

        /// <summary>
        /// 字符串的MD5签名
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toLower"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ComputeMd5Hash(this string text, bool toLower = true, Encoding? encoding = null)
        {
            using var md5 = MD5.Create();
            return HashString(md5, text, toLower, encoding);
        }

        /// <summary>
        /// 文件流MD5加密
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string ComputeMd5Hash(this Stream stream, bool toLower = true)
        {
            using var md5 = MD5.Create();
            return HashSteam(md5, stream, toLower);
        }

        /// <summary>
        /// 文件流SHA256加密
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string ComputeSha256Hash(this Stream stream, bool toLower = true)
        {
            using var sha256 = SHA256.Create();
            return HashSteam(sha256, stream, toLower);
        }
    }
}
