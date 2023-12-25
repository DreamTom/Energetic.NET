using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        private static T? GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString()).First();
            return memInfo.GetCustomAttribute<T>();
        }

        /// <summary>
        /// 获取枚举值描述特性信息
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var attr = GetAttributeOfType<DescriptionAttribute>(enumValue);
            if (attr == null)
                return enumValue.ToString();
            return attr.Description;
        }

        /// <summary>
        /// 获取枚举值展示特性信息
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            var attr = GetAttributeOfType<DisplayNameAttribute>(enumValue);
            if (attr == null)
                return enumValue.ToString();
            return attr.DisplayName;
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }
    }
}
