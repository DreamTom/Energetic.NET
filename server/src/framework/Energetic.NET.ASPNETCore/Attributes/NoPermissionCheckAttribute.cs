namespace Energetic.NET.ASPNETCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class NoPermissionCheckAttribute : Attribute
    {
    }
}
