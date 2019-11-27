namespace Axle.Conversion.Binding
{
    /// <summary>
    /// An interface representing a <see cref="IBinderValueProvider" />, that is a binder value provider used to 
    /// supply data for simple data types.
    /// </summary>
    public interface IBinderValueProvider : IBinderDataProvider
    {
        /// <summary>
        /// A string representation of the value represented by the current <see cref="IBinderValueProvider"/> implementation.
        /// </summary>
        string Value { get; }
    }
}
