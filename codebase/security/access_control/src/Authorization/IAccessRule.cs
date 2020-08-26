namespace Axle.Security.AccessControl.Authorization
{
    /// <summary>
    /// An interface that represents an access rule; that is an object which describes the associated <see cref="IAccessRight">access rights</see> to an access level.
    /// </summary>
    /// <seealso cref="IAccessRight"/>
    /// <seealso cref="IAccessLevelEntry"/>
    public interface IAccessRule
    {
        /// <summary>
        /// The name of the access level the current rule apples to.
        /// </summary>
        string AccessLevel { get; }
        /*
        /// <summary>
        /// A collection of <see cref="IAccessRight">access right</see> objects, representing the rights associated with the current <see cref="AccessLevel">access level</see>.
        /// </summary>
        IEnumerable<IAccessRight> AccessRights { get; }*/
    }
}