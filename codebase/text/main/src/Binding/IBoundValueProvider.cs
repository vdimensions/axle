namespace Axle.Text.Data.Binding
{
    /// <summary>
    /// An interface representing a member value provider; that is an object which provides values for 
    /// object members to a <see cref="IBinder"/> during the binding process.
    /// </summary>
    /// <seealso cref="IBinder"/>
    public interface IBoundValueProvider
    {
        /// <summary>
        /// The name of the member this <see cref="IBoundValueProvider"/> is providing values for.
        /// </summary>
        /// <remarks>
        /// An <see cref="string.Empty">empty string</see> (<c>""</c>) value is allowed when the current 
        /// <see cref="IBoundValueProvider"/> addresses the root object being bound.
        /// </remarks>
        string Name { get; }
    }
}
