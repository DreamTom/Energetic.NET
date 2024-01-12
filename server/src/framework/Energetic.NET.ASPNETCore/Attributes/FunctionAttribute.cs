namespace Energetic.NET.ASPNETCore.Attributes
{
    /// <summary>
    /// 模块描述
    /// </summary>
    /// <param name="name">模块名称</param>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FunctionAttribute(string name) : Attribute
    {
        public string Name { get; init; } = name;
    }
}
