namespace Axle.Reflection
{
    /// <summary>
    /// An interface that represents a reflected member (usually a method or constructor) that can invoked trough reflection.
    /// </summary>
    //[Maturity(CodeMaturity.Stable)]
    public interface IInvokable
    {
        //IEnumerable<IParameter> Parameters { get; }

        /// <summary>
        /// Returns an array of <see cref="IParameter" /> instances, each representing a parameter to the current 
        /// <see cref="IParameter" /> implementation. If the method has no parameters, an empty array is returned.
        /// </summary>
        /// <returns>
        /// An array of <see cref="IMethod" /> instances, each representing a parameter to the current 
        /// <see cref="IMethod" /> implementation, or an empty array if the method has no parameters.
        /// </returns>
        IParameter[] GetParameters();

        /// <summary>
        /// Invokes the current <see cref="IInvokable" /> implementation.
        /// </summary>
        /// <param name="target">
        /// If the reflected member is an instance method, represents the target object upon whose behalf the method is invoked on 
        /// (and would be referenced by the <c>this</c> keyword in the reflected code). <br />
        /// </param>
        /// <param name="args">
        /// An array of variable length that represents the values of any parameters that the reflected invokable may have. <br />
        /// The number of values supplied must match exactly the number of parameters of the reflected invokable.
        /// </param>
        /// <returns>
        /// If the reflected invokable is a constructor, returns the newly constructed instance;<br/>
        /// if the reflected invokable is a method, and that method has a specific return value, then the result will
        /// contain return value of the method, otherwise the value returned will be <c>null</c>.
        /// </returns>
        /// <remarks>
        /// If the reflected member is a static method, always use <c>null</c> for the <paramref name="target"/> parameter.<br/>
        /// Reflected constructors will ignore any value passed to the the <paramref name="target"/> parameter.
        /// </remarks>
        object Invoke(object target, params object[] args);
    }
}