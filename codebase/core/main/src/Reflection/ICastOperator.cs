#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a cast operator defined by a <see cref="System.Type">type</see>.
    /// </summary>
    /// <seealso cref="ICastOperator{T1,T2}"/>
    public interface ICastOperator
    {
        /// <summary>
        /// Invokes the cast operation represented by the current <see cref="ICastOperator">cast operator</see> on the target object.
        /// </summary>
        /// <param name="target">
        /// The target object to be cast. 
        /// </param>
        /// <returns>
        /// An object that is the result of the cast operation represented by the current <see cref="ICastOperator">cast operator</see> instance. 
        /// </returns>
        object Invoke(object target);
        bool TryInvoke(object target, out object result);

        /// <summary>
        /// Determines if the current <see cref="ICastOperator">cast operator</see> is defined by the target <see cref="System.Type">type</see>.
        /// </summary>
        bool IsDefined { get; }
    }

    /// <summary>
    /// A generic interface representing a cast operator defined by a <see cref="System.Type">type</see>.
    /// </summary>
    /// <typeparam name="T1">The source type for the type cast represented by the current <see cref="ICastOperator{T1,T2}">cast operator</see>.</typeparam>
    /// <typeparam name="T2">The target type for the type cast represented by the current <see cref="ICastOperator{T1,T2}">cast operator</see>.</typeparam>
    /// <seealso cref="ICastOperator"/>
    public interface ICastOperator<T1, T2> : ICastOperator
    {
        T2 Invoke(T1 target);
        bool TryInvoke(T1 target, out T2 result);
    }
}
#endif