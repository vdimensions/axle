namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected read-only property member.
    /// </summary>
    /// <seealso cref="System.Reflection.PropertyInfo"/>
    public interface IReadOnlyProperty : IProperty, IReadableMember { }
}