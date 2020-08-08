using System;

namespace Axle.Reflection
{
    /// <summary>
    /// An interface for introspecting a generic type. The <see cref="IGenericTypeIntrospector"/> can represent 
    /// either a purely generic type definition, or a type with partially or fully substituted generic arguments.
    /// Refer to its <see cref="GenericDefinitionIntrospector"/> property to access the purely generic type definition,
    /// or make calls to the <see cref="MakeGenericType(Type[])"/> method to incrementally supply generic types.
    /// </summary>
    public interface IGenericTypeIntrospector
    {
        /// <summary>
        /// Gets a <see cref="ITypeIntrospector"/> instance produced from the current <see cref="IGenericTypeIntrospector"/>'s
        /// <see cref="GenericDefinitionType"/> and <see cref="GenericTypeArguments"/>.
        /// If the current <see cref="IGenericMethod"/> has no or partially applied generic types,
        /// then <c>null</c> will be returned.
        /// </summary>
        /// <returns>
        /// A <see cref="ITypeIntrospector"/> instance produced from the current <see cref="IGenericTypeIntrospector"/>'s
        /// <see cref="GenericDefinitionType"/> and <see cref="GenericTypeArguments"/>
        /// - OR -
        /// <c>null</c> if the current <see cref="IGenericTypeIntrospector"/> has no or partially applied generic types.
        /// </returns>
        ITypeIntrospector Introspect();

        /// <summary>
        /// Incrementally applies the provided <paramref name="typeParameters"/>, appending them to the existing
        /// <see cref="GenericTypeArguments"> generic type argument list</see> and so produces 
        /// a new <see cref="IGenericTypeIntrospector"/> instance.
        /// </summary>
        /// <param name="typeParameters">
        /// A list of <see cref="Type">type</see> parameters to append to the <see cref="GenericDefinitionType"/>
        /// of the current <see cref="IGenericTypeIntrospector"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="IGenericTypeIntrospector"/> instance with its 
        /// <see cref="GenericTypeArguments">generic type arguments list</see> completed with the provided
        /// <paramref name="typeParameters"/>
        /// </returns>
        /// <remarks>
        /// Calls to this method will not modify the state of the current <see cref="IGenericTypeIntrospector"/> instance;
        /// a new object is produced with each call. The only exception is when the <paramref name="typeParameters"/> is
        /// omitted or passed as empty array -- in such case the current instance is returned.
        /// </remarks>
        IGenericTypeIntrospector MakeGenericType(params Type[] typeParameters);

        /// <summary>
        /// Gets an array of the currently applied generic type arguments.
        /// </summary>
        Type[] GenericTypeArguments { get; }
        
        /// <summary>
        /// Gets a reference to the generic type definition represented by the current
        /// <see cref="IGenericTypeIntrospector"/> instance.
        /// </summary>
        Type GenericDefinitionType { get; }
        
        /// <summary>
        /// Gets the <see cref="IGenericTypeIntrospector"/> for the <see cref="GenericDefinitionType"/>.
        /// </summary>
        IGenericTypeIntrospector GenericDefinitionIntrospector { get; }
    }
}