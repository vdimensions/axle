namespace Axle.Conversion.Binding
{
    /// <summary>
    /// An interface representing a binder data provider; that is an object which provides data to a <see cref="IBinder"/> during the binding process.
    /// </summary>
    /// <seealso cref="IBinder"/>
    public interface IBinderDataProvider
    {
        /// <summary>
        /// The name of the member this <see cref="IBinderDataProvider"/> is providing values for.
        /// </summary>
        /// <remarks>
        /// An <see cref="string.Empty">empty string</see> (<c>""</c>) value is allowed when the current <see cref="IBinderDataProvider"/>
        /// addresses the root object being bound.
        /// </remarks>
        string Name { get; }
    }
}
