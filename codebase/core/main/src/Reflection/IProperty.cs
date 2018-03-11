namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected property member.
    /// </summary>
    /// <seealso cref="System.Reflection.PropertyInfo"/>
    public interface IProperty : IReadableMember, IWriteableMember, IAttributeTarget { }
}