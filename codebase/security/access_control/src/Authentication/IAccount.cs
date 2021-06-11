using Axle.Security.AccessControl.Authorization;

namespace Axle.Security.AccessControl.Authentication
{
    /// <summary>
    /// A <see cref="IPrincipal">principal</see> which is a subject to authentication. An account could represent
    /// a person or a service.
    /// From security perspective, accounts are the initiators of actions which can be a subject to security
    /// permissions. 
    /// </summary>
    /// <seealso cref="IPrincipal"/>
    public interface IAccount : IPrincipal, IGroupMember
    {
        IRole Role { get; }
    }
}