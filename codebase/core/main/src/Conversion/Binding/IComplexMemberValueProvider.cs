namespace Axle.Conversion.Binding
{
    /// <summary>
    /// An interface representing an binder object provider; that is a type of <see cref="IBindingMemberValueProvider"/> that is used to
    /// supply values for complex types.
    /// </summary>
    public interface IComplexMemberValueProvider : IBindingMemberValueProvider
    {
        /// <summary>
        /// Attempts to get the value(s) associated for the given <paramref name="member"/>.
        /// </summary>
        /// <param name="member">
        /// The member to lookup values for.
        /// </param>
        /// <param name="value">
        /// An <see cref="IBindingMemberValueProvider"/> instnce representing the value associated with the given <paramref name="member"/>
        /// </param>
        /// <returns>
        /// <c>true</c>, if there are values for the given <paramref name="member"/>; <c>false</c> otherwise.
        /// </returns>
        bool TryGetValue(string member, out IBindingMemberValueProvider value);

        /// <summary>
        /// Gets the value(s) associated for the given <paramref name="member"/>.
        /// </summary>
        /// <param name="member">
        /// The member to lookup values for.
        /// </param>
        /// <returns>
        /// An <see cref="IBindingMemberValueProvider"/> instnce representing the values associated with the given <paramref name="member"/>, 
        /// or <c>null</c> if no values exist for the given member.
        /// </returns>
        IBindingMemberValueProvider this[string member] { get; }
    }
}
