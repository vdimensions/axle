namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected property member.
    /// </summary>
    /// <seealso cref="System.Reflection.PropertyInfo"/>
    //[Maturity(CodeMaturity.Stable)]
    public interface IProperty : IReadableMember, IWriteableMember, IAttributeTarget { }
}