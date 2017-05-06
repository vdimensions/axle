using Axle.Verification;


namespace Axle.Conversion
{
#if !netstandard
    [System.Serializable]
#endif
    public sealed class ChainedConverter<T, T1, TResult> : AbstractConverter<T,TResult>
    {
        private readonly IConverter<T, T1> converter1;
        private readonly IConverter<T1, TResult> converter2;

        internal ChainedConverter(IConverter<T, T1> converter1, IConverter<T1, TResult> converter2)
        {
            this.converter1 = converter1.VerifyArgument(nameof(converter1)).IsNotNull().Value;
            this.converter2 = converter2.VerifyArgument(nameof(converter1)).IsNotNull().Value;
        }

        protected override TResult DoConvert(T source)
        {
            return converter2.Convert(converter1.Convert(source));
        }
    }

    public static class ChainedConverter
    {
        public static IConverter<T, TResult> Create<T, T1, TResult>(IConverter<T, T1> converter1, IConverter<T1, TResult> converter2)
        {
            return new ChainedConverter<T, T1, TResult>(converter1, converter2);
        }
        public static IConverter<T, TResult> Create<T, T1, T2, TResult>(IConverter<T, T1> converter1, IConverter<T1, T2> converter2, IConverter<T2, TResult> converter3)
        {
            return Create(Create(converter1, converter2), converter3);
        }
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