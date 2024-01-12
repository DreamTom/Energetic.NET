namespace Energetic.NET.ASPNETCore.Attributes
{
    /// <summary>
    /// 功能描述
    /// </summary>
    /// <param name="name">功能名称</param>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute(string name) : Attribute
    {
        public string Name { get; init; } = name;
    }
}
