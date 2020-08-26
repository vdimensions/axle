namespace Axle.Security.AccessControl.Authorization
{
    public interface IAccessPolicy : IAccessRule
    {
        bool BypassElevation { get; }
    }
}