using Axle.Security.AccessControl.Authentication;

namespace Axle.Security.AccessControl.Authorization
{
    /// <summary>
    /// A marker interface. Identifies security principals that can be assigned to a <see cref="IGroup">group</see>.
    /// A single group member may participate in multiple groups. Because the group is itself a principal
    /// with its own permission set, each group member will inherit the permission set of any group it
    /// is a member of. 
    /// <para>
    /// Notable implementations of this interface include: 
    /// <list type="bullet">
    ///   <item><description><see cref="IAccount"/></description></item>
    ///   <item><description><see cref="IRole"/></description></item>
    /// </list>
    /// </para>
    /// </summary>
    /// <seealso cref="IGroup" />
    /// <seealso cref="IRole"/>
    /// <seealso cref="IAccount"/>
    public interface IGroupMember
    {
    }
}