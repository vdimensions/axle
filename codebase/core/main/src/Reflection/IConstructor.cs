#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected constructor.
    /// </summary>
    /// <seealso cref="System.Reflection.ConstructorInfo"/>
    public interface IConstructor : IInvokable, IMember, IAttributeTarget
    {
        /// <summary>
        /// Invokes the current <see cref="IConstructor" /> implementation.
        /// </summary>
        /// <param name="args">
        /// An array of variable length that represents the values of any parameters that the reflected constructor may have. <br />
        /// The number of values supplied must match exactly the number of parameters of the reflected constructor.
        /// </param>
        /// <returns>
        /// Returns the newly constructed object instance;<br/>
        /// </returns>
        object Invoke(params object[] args);
    }
}
#endif