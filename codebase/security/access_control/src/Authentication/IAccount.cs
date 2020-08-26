using Axle.Security.AccessControl.Authorization;

namespace Axle.Security.AccessControl.Authentication
{
    public interface IAccount : IPrincipal, IGroupMember
    {
        IRole Role { get; }
    }
}