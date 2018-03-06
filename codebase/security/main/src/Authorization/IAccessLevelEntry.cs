namespace Axle.Security.Authorization
{
    /// <summary>
    /// Represents an object that has a security level of access associated with, and an owner principal. 
    /// <para>
    /// The permissions to manipulate an <see cref="IAccessLevelEntry">access level entry object</see> are determined by the respective
    /// access level that is associated with the object.
    /// </para>
    /// <para>
    /// An <see cref="IAccessLevelEntry">access level entry object</see> also has an owner <see cref="IPrincipal">principal</see> who is granted complete access over the object, 
    /// regardless of the permission set associated with the object's access level.
    /// </para>
    /// </summary>
    /// <seealso cref="IPrincipal"/>
    public interface IAccessLevelEntry
    {
        /// <summary>
        /// A string representing the object's access level. An access level can be assigned a permission set, which is checked against any principal
        /// who attempts to manipulate the <see cref="IAccessLevelEntry">access level entry object</see>.
        /// </summary>
        string AccessLevel { get; }

        /// <summary>
        /// The name of the owner <see cref="IPrincipal">principal</see> of the object.
        /// </summary>
        string Owner { get; }
    }
}
