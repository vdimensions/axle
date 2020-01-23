namespace Axle.Text.Data.Binding
{
    /// <summary>
    /// The default implementation of the <see cref="IBoundSimpleValueProvider"/> interface.
    /// </summary>
    public sealed class BoundSimpleValueProvider : IBoundSimpleValueProvider
    {
        /// <summary>
        /// Creates a new instance of the <see cref="BoundSimpleValueProvider"/> class.
        /// </summary>
        /// <param name="name">The name of the represented member.</param>
        /// <param name="value">The raw value of the represented member.</param>
        public BoundSimpleValueProvider(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Value { get; }
    }
}