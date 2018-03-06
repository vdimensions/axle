using Axle.Security.Authorization;


namespace Axle.Security.Authentication
{
    public interface IAccount : IPrincipal, IGroupMember
    {
        IRole Role { get; }
    }
}