using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Common.Attributes
{
    /// <summary>
    /// 表名前缀
    /// </summary>
    /// <param name="prefix"></param>
    [AttributeUsage(AttributeTargets.Class)]
    public class TablePrefixAttribute(string prefix) : Attribute
    {
        public string Prefix { get; set; } = prefix;
    }
}
