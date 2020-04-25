namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// An interface representing a <see cref="IBoundSimpleValueProvider" />, that is a binder value provider used to 
    /// supply data for simple data types.
    /// </summary>
    public interface IBoundSimpleValueProvider : IBoundValueProvider
    {
        /// <summary>
        /// A string representation of the value represented by the current <see cref="IBoundSimpleValueProvider"/>   
        /// implementation.
        /// </summary>
        string Value { get; }
    }
}
