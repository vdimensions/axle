namespace Axle.Security.AccessControl.Authorization
{
    /// <summary>
    /// A group is a collective representation of different principals, which
    /// similarly to any other <see cref="IPrincipal">principal</see> can be assigned permissions.
    /// All principals that are <see cref="IGroupMember">members</see> of a <see cref="IGroup">group</see>
    /// automatically inherit the access rules defined for that group.
    /// </summary>
    public interface IGroup : IPrincipal
    {
    }
}