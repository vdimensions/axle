using System;
using System.Reflection;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface that represents a reflected method. Enables quering the reflected metadata and allows 
    /// the invocation of the reflected member.
    /// </summary>
    /// <seealso cref="IInvokable"/>
    /// <seealso cref="IMember"/>
    /// <seealso cref="IAttributeTarget"/>
    /// <seealso cref="MethodInfo"/>
    public interface IMethod : IMember, IReflected<MethodInfo>, IInvokable, IAttributeTarget
    {
        /// <summary>
        /// Substitutes the elements of an array of types for the type parameters of the current generic method definition, 
        /// and returns a <see cref="IGenericMethod"/> object representing the resulting constructed method.
        /// </summary>
        /// <param name="types">
        /// An array of types to be substituted for the type parameters of the generic method definition.
        /// </param>
        /// <returns>
        /// A <see cref="IGenericMethod"/> object representing the resulting constructed method.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IMethod">method</see> is already an instance of <see cref="IGenericMethod"/>
        /// <para>-OR-</para>
        /// The current <see cref="IMethod">method</see> does not accept type parameters (<see cref="IsGeneric"/> is <c>false</c>).
        /// </exception>
        /// <seealso cref="IGenericMethod"/>
        /// <seealso cref="MethodInfo.MakeGenericMethod"/>
        IGenericMethod MakeGeneric(params Type[] types);

        /// <summary>
        /// Gets a value indicating whether the current <see cref="IMethod">method</see> is generic.
        /// </summary>
        /// <seealso cref="MethodBase.IsGenericMethod"/>
        bool IsGeneric { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="IMethod">method</see> is generic method definition.
        /// </summary>
        /// <seealso cref="MethodBase.IsGenericMethodDefinition"/>
        bool HasGenericDefinition { get; }

        /// <summary>
        /// Gets the return type of the current <see cref="IMethod">method</see>.
        /// </summary>
        /// <seealso cref="MethodInfo.ReturnType"/>
        Type ReturnType { get; }
    }
}