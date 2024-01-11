namespace Energetic.NET.ASPNETCore
{
    /// <summary>
    /// 无权限校验，只要已经登录就可以
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class NoPermissionCheckAttribute : Attribute
    {
    }
}
