using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected parameter to a constructor or method and discovers parameter metadata. 
    /// </summary>
    /// <seealso cref="System.Reflection.ParameterInfo"/>
    public interface IParameter : IAttributeTarget
    {
        /// <summary>
        /// The type of the current <see cref="IParameter">parameter</see>. 
        /// </summary>
        /// <seealso cref="System.Reflection.ParameterInfo.ParameterType"/>
        Type Type { get; }

        /// <summary>
        /// The name of the current <see cref="IParameter">parameter</see>. 
        /// </summary>
        /// <seealso cref="System.Reflection.ParameterInfo.Name"/>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="IParameter">parameter</see> is optional to the corresponding constructor or method.
        /// </summary>
        /// <seealso cref="System.Reflection.ParameterInfo.IsOptional"/>
        bool IsOptional { get; }

        /// <summary>
        /// Gets the default value of the parameter if it has one. 
        /// </summary>
        /// <seealso cref="System.Reflection.ParameterInfo.DefaultValue"/>
        object DefaultValue { get; }

        /// <summary>
        /// Gets a value indicating the direction of the current <see cref="IParameter">parameter</see>.
        /// </summary>
        ParameterDirection Direction { get; }
    }
}