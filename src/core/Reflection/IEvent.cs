namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected event member.
    /// </summary>
    /// <seealso cref="System.Reflection.EventInfo"/>
    //[Maturity(CodeMaturity.Stable)]
    public interface IEvent : ICombineRemoveMember, IAttributeTarget { }
}