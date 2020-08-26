using Axle.Security.AccessControl.Authentication;
using Axle.Security.AccessControl.Authorization;

namespace Axle.Security.AccessControl
{
    /// <summary>
    /// Represents a security principal; that is an entity which has security information assigned to it.
    /// <para>
    /// Notable implementations of this interface include:
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="IAccount" /></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="IGroup" /></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="IRole" /></description>
    ///   </item>
    /// </list>
    /// </para>
    /// </summary>
    /// <seealso cref="IAccount"/>
    /// <seealso cref="IGroup"/>
    /// <seealso cref="IRole"/>
    public interface IPrincipal
    {
        /// <summary>
        /// Gets the name, which is used to uniquely identify the current principal.
        /// </summary>
        string Name { get; }
    }
}