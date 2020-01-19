using System;

namespace Axle.Reflection
{
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
        /// <c>null</c> if the current <see cref="IGenericMethod"/> has no or partially applied generic types.
        /// </returns>
        ITypeIntrospector GetIntrospector();
        
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
    }
}