namespace Axle.Security.Authorization
{
    public interface IAccessPolicy : IAccessRule
    {
        bool BypassElevation { get; }
    }
}