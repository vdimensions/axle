using Axle.Security.AccessControl.Authentication;

namespace Axle.Security.AccessControl.Authorization
{
    /// <summary>
    /// A marker interface. Identifies security entries that can be assigned to a <see cref="IGroup">group</see>.
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