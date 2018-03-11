namespace Axle.Reflection
{
    /// <summary>
    /// An interface that represents a get accessor (or getter) for a class member - a property or a field.
    /// </summary>
    /// <seealso cref="AccessorType.Get">AccessorType.Get</seealso>
    /// <seealso cref="ISetAccessor" />
    /// <seealso cref="IAccessor" />
    public interface IGetAccessor : IAccessor
    {
        /// <summary>
        /// Invokes the appropriate mechanism of the reflected getter. For properties, this will invoke the 
        /// property's get method, for fields it will return the field's value.
        /// </summary>
        /// <param name="target">
        /// The target object that owns the member (field or property) whose accessor is represented by the current <see cref="IGetAccessor"/>
        /// instance.
        /// </param>
        /// <returns>
        /// The result of the get method if the current <see cref="IGetAccessor"/> represents a property; or the value of the represented field.
        /// </returns>
        object GetValue(object target);
    }
}