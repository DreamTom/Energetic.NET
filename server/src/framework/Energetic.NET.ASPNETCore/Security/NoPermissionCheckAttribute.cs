namespace Energetic.NET.ASPNETCore.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class NoPermissionCheckAttribute : Attribute
    {
    }
}
