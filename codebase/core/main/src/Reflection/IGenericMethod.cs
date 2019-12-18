using System;

namespace Axle.Reflection
{
    /// <summary>
    /// Represents a generic method. A generic method is produced from a 'raw' method containing any generic parameter 
    /// placeholders, along with the respective placeholder substitution types.
    /// </summary>
    public interface IGenericMethod : IMethod
    {
        /// <summary>
        /// Gets an array of types representing the concrete types which substitute the respective generic type slot.
        /// </summary>
        Type[] GenericArguments { get; }
        /// <summary>
        /// Gets a reference to the non-generic 'raw' method this instance is produced from.
        /// </summary>
        IMethod RawMethod { get; }
    }
}