#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using Axle.Conversion;
using Axle.Data.Records.Mapping.Accessors.DataFields;
using Axle.Verification;

namespace Axle.Data.Records.Mapping.Accessors
{
    /// <summary>
    /// A static class to contain extension methods for <see cref="IDataFieldAccessor{T}"/> instances.
    /// </summary>
    public static class DataFieldAccessorExtensions
    {
        private sealed class DataFieldTransformingAccessor<T1, T2> : IDataFieldAccessor<T2>, IFieldTypeOverride
        {
            private readonly IDataFieldAccessor<T1> _delegatingAccessor;
            private readonly ITwoWayConverter<T1, T2> _converter;

            public DataFieldTransformingAccessor(IDataFieldAccessor<T1> delegatingAccessor, ITwoWayConverter<T1, T2> converter)
            {
                _delegatingAccessor = delegatingAccessor;
                _converter = converter;
            }

            public T2 GetValue(DataRecord dataRow, string key) => _converter.Convert(_delegatingAccessor.GetValue(dataRow, key));

            public void SetValue(DataRecord dataRow, string key, T2 value) => _delegatingAccessor.SetValue(dataRow, key, _converter.ConvertBack(value));
            Type IFieldTypeOverride.OverridenFieldType => typeof(T1);
        }
        
        /// <summary>
        /// Combines the provided by the <paramref name="dataFieldAccessor"/> parameter
        /// <see cref="IDataFieldAccessor{T}"/> instance with the <see cref="ITwoWayConverter{T,TResult}"/> that is
        /// supplied by the <paramref name="converter"/> parameter into a new <see cref="IDataFieldAccessor{TResult}"/>
        /// instance that is capable to get and set data fields of the resulting type <typeparamref name="TResult"/>.
        /// Internally the fields are interpreted as values of <typeparamref name="T"/> and passed in to the
        /// <paramref name="converter"/> to produce the <typeparamref name="TResult"/> value, and the other way around
        /// - when setting a value of type <typeparamref name="TResult"/>, it will be stored as a value of type
        /// <typeparamref name="T"/>. 
        /// </summary>
        /// <param name="dataFieldAccessor">
        /// The <see cref="IDataFieldAccessor{T}"/> instance, which is capable of working with values of type
        /// <typeparamref name="T"/>.
        /// </param>
        /// <param name="converter">
        /// Am <see cref="ITwoWayConverter{T,TResult}"/> which will be used to perform the conversions between
        /// values of <typeparamref name="T"/> and <typeparamref name="TResult"/>. 
        /// </param>
        /// <typeparam name="T">The type of values supported by the <paramref name="dataFieldAccessor"/></typeparam>
        /// <typeparam name="TResult">
        /// The type of value supported by the resulting <see cref="IDataFieldAccessor{T}"/> instance.
        /// </typeparam>
        /// <returns>
        /// A new <see cref="IDataFieldAccessor{TResult}"/> instance that is capable to get and set data fields of the
        /// resulting type <typeparamref name="TResult"/>.
        /// </returns>
        public static IDataFieldAccessor<TResult> Transform<T, TResult>(
            this IDataFieldAccessor<T> dataFieldAccessor,
            ITwoWayConverter<T, TResult> converter)
        {
            dataFieldAccessor.VerifyArgument(nameof(dataFieldAccessor)).IsNotNull();
            converter.VerifyArgument(nameof(converter)).IsNotNull();
            return new DataFieldTransformingAccessor<T, TResult>(dataFieldAccessor, converter);
        }
    }
}
#endif