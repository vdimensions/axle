using Axle.Verification;


namespace Axle.Conversion
{
    /// <summary>
    /// A special converter implementation that chains together two <see cref="IConverter{TSource,TTarget}"/> instances.
    /// Useful if the conversion from an instance of a given type <typeparamref name="T" /> must be converted to an intermediate
    /// type <typeparamref name="TIntermediate"/> with one <see cref="IConverter{T,T1}">converter</see> before that conversion result 
    /// is converted to the desired type <typeparamref name="TResult"/> with <see cref="IConverter{T1,TResult}">another converter</see>.
    /// <para>
    /// Multiple chained converters can be used together to cover a more complex conversion with more than one intermediate object.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of the source object to be converted. </typeparam>
    /// <typeparam name="TIntermediate">The type of an intermediate object to convert the source object to. </typeparam>
    /// <typeparam name="TResult">The resulting type of the conversion, produced by converting the intermediate object. </typeparam>
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class ChainedConverter<T, TIntermediate, TResult> : AbstractConverter<T,TResult>
    {
        private readonly IConverter<T, TIntermediate> _converter1;
        private readonly IConverter<TIntermediate, TResult> _converter2;

        internal ChainedConverter(IConverter<T, TIntermediate> converter1, IConverter<TIntermediate, TResult> converter2)
        {
            _converter1 = converter1.VerifyArgument(nameof(converter1)).IsNotNull().Value;
            _converter2 = converter2.VerifyArgument(nameof(converter2)).IsNotNull().Value;
        }

        protected override TResult DoConvert(T source)
        {
            return _converter2.Convert(_converter1.Convert(source));
        }
    }

    /// <summary>
    /// A static class providing helper methods related to the <see cref="ChainedConverter{T1,T2,T3}"/> class.
    /// </summary>
    public static class ChainedConverter
    {
        /// <summary>
        /// Chains a series of converters to allow them to be interfaced as a single <see cref="IConverter{T,TResult}"/> implementation.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the source object to be converted
        /// </typeparam>
        /// <typeparam name="T1">
        /// A type that is the result of an intermediate covnersion.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the final converesion result
        /// </typeparam>
        /// <param name="converter1">
        /// The first converter to put to the conversion chain.
        /// </param>
        /// <param name="converter2">
        /// The last converter to put to the conversion chain.
        /// </param>
        /// <returns>
        /// Series of converters interfaced as a single <see cref="IConverter{T,TResult}"/> implementation
        /// </returns>
        public static IConverter<T, TResult> Create<T, T1, TResult>(IConverter<T, T1> converter1, IConverter<T1, TResult> converter2)
        {
            return new ChainedConverter<T, T1, TResult>(converter1, converter2);
        }
        /// <summary>
        /// Chains a series of converters to allow them to be interfaced as a single <see cref="IConverter{T,TResult}"/> implementation.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the source object to be converted
        /// </typeparam>
        /// <typeparam name="T1">
        /// A type that is the result of an intermediate covnersion.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type that is the result of an intermediate covnersion.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the final converesion result
        /// </typeparam>
        /// <param name="converter1">
        /// The first converter to put to the conversion chain.
        /// </param>
        /// <param name="converter2">
        /// The second converter to put to the conversion chain.
        /// </param>
        /// <param name="converter3">
        /// The last converter to put to the conversion chain.
        /// </param>
        /// <returns>
        /// Series of converters interfaced as a single <see cref="IConverter{T,TResult}"/> implementation
        /// </returns>
        public static IConverter<T, TResult> Create<T, T1, T2, TResult>(IConverter<T, T1> converter1, IConverter<T1, T2> converter2, IConverter<T2, TResult> converter3)
        {
            return Create(Create(converter1, converter2), converter3);
        }
        /// <summary>
        /// Chains a series of converters to allow them to be interfaced as a single <see cref="IConverter{T,TResult}"/> implementation.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the source object to be converted
        /// </typeparam>
        /// <typeparam name="T1">
        /// A type that is the result of an intermediate covnersion.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type that is the result of an intermediate covnersion.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type that is the result of an intermediate covnersion.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the final converesion result
        /// </typeparam>
        /// <param name="converter1">
        /// The first converter to put to the conversion chain.
        /// </param>
        /// <param name="converter2">
        /// The second converter to put to the conversion chain.
        /// </param>
        /// <param name="converter3">
        /// The third converter to put to the conversion chain.
        /// </param>
        /// <param name="converter4">
        /// The last converter to put to the conversion chain.
        /// </param>
        /// <returns>
        /// Series of converters interfaced as a single <see cref="IConverter{T,TResult}"/> implementation
        /// </returns>
        public static IConverter<T, TResult> Create<T, T1, T2, T3, TResult>(
            IConverter<T, T1> converter1, 
            IConverter<T1, T2> converter2, 
            IConverter<T2, T3> converter3, 
            IConverter<T3, TResult> converter4)
        {
            return Create(Create(converter1, converter2), Create(converter3, converter4));
        }
    }
}