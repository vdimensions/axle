namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a setter method (or setter) for a class member (a field or property).
    /// </summary>
    /// <seealso cref="AccessorType.Set">AccessorType.Set</seealso>
    /// <seealso cref="IGetAccessor" />
    /// <seealso cref="IAccessor" />
    //[Maturity(CodeMaturity.Stable)]
    public interface ISetAccessor : IAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        void SetValue(object target, object value);
    }
}