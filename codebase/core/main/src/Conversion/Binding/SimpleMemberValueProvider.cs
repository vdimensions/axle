namespace Axle.Conversion.Binding
{
    /// <summary>
    /// The default implementation of the <see cref="ISimpleMemberValueProvider"/> interface.
    /// </summary>
    public sealed class SimpleMemberValueProvider : ISimpleMemberValueProvider
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SimpleMemberValueProvider"/> class.
        /// </summary>
        /// <param name="name">The name of the represented member.</param>
        /// <param name="value">The raw value of the represented member.</param>
        public SimpleMemberValueProvider(string name, string value)
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