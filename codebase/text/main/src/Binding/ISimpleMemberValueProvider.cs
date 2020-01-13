namespace Axle.Text.StructuredData.Binding
{
    /// <summary>
    /// An interface representing a <see cref="ISimpleMemberValueProvider" />, that is a binder value provider used to 
    /// supply data for simple data types.
    /// </summary>
    public interface ISimpleMemberValueProvider : IBindingValueProvider
    {
        /// <summary>
        /// A string representation of the value represented by the current <see cref="ISimpleMemberValueProvider"/>   
        /// implementation.
        /// </summary>
        string Value { get; }
    }
}
