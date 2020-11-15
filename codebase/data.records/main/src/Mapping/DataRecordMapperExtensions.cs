#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Linq.Expressions;
using Axle.Conversion;
using Axle.Data.Records.Mapping.Accessors;
using Axle.Data.Records.Mapping.Accessors.DataFields;
using Axle.Text.Parsing;

namespace Axle.Data.Records.Mapping
{
    /// <summary>
    /// A static class containing commonly used extensions to the <see cref="DataRecordMapper{T}"/> type.
    /// </summary>
    public static class DataRecordMapperExtensions
    {
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, bool>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new BooleanDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, char>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new CharacterDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, byte>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new ByteDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, short>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new Int16DataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, int>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new Int32DataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, long>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new Int64DataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, TimeSpan>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(
                name, 
                new Int64DataFieldAccessor().Transform(new TimeSpanToTicksConverter()), 
                expression);
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, float>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new SingleDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, double>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DoubleDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, decimal>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DecimalDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, string>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new StringDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, Guid>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new GuidDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, DateTime>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DateTimeDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, DateTimeOffset>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DateTimeOffsetDataFieldAccessor(), expression);

        public static void RegisterEnumNameFieldAccessor<T, TEnum>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new StringDataFieldAccessor().Transform(CombinedConverter.Create(
                    new EnumParser<TEnum>(), 
                    new EnumToStringConverter<TEnum>())), 
                expression);
        }
        
        public static void RegisterSByteEnumFieldAccessor<T, TEnum>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new SByteDataFieldAccessor().Transform(new EnumToSByteConverter<TEnum>()), 
                expression);
        }
        
        public static void RegisterByteEnumFieldAccessor<T, TEnum>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new ByteDataFieldAccessor().Transform(new EnumToByteConverter<TEnum>()), 
                expression);
        }
        
        public static void RegisterInt16EnumFieldAccessor<T, TEnum>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new Int16DataFieldAccessor().Transform(new EnumToInt16Converter<TEnum>()), 
                expression);
        }
        public static void RegisterInt32EnumFieldAccessor<T, TEnum>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new Int32DataFieldAccessor().Transform(new EnumToInt32Converter<TEnum>()), 
                expression);
        }
        public static void RegisterInt64EnumFieldAccessor<T, TEnum>(this DataRecordMapper<T> mapper, string name, Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new Int64DataFieldAccessor().Transform(new EnumToInt64Converter<TEnum>()), 
                expression);
        }
    }
}
#endif