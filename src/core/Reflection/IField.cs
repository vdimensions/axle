namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected field member.
    /// </summary>
    /// <seealso cref="System.Reflection.FieldInfo"/>
    //[Maturity(CodeMaturity.Stable)]
    public interface IField : IReadWriteMember, IAttributeTarget { }
}
