#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Axle.Conversion;
using Axle.Data.Records.Mapping.Accessors;
using Axle.Data.Records.Mapping.Accessors.DataFields;
using Axle.Reflection.Extensions.Type;
using Axle.Text.Parsing;

namespace Axle.Data.Records.Mapping
{
    /// <summary>
    /// A static class containing commonly used extensions to the <see cref="DataRecordMapper{T}"/> type.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class DataRecordMapperExtensions
    {
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, bool>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new BooleanDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, char>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new CharacterDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, byte>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new ByteDataFieldAccessor(), expression);

        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, sbyte>> expression)
            where T : class
            => mapper.RegisterFieldAccessor(name, new SByteDataFieldAccessor(), expression);

        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, short>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new Int16DataFieldAccessor(), expression);

        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, ushort>> expression)
            where T : class
            => mapper.RegisterFieldAccessor(name, new UInt16DataFieldAccessor(), expression);

        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, int>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new Int32DataFieldAccessor(), expression);

        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, uint>> expression)
            where T : class
            => mapper.RegisterFieldAccessor(name, new UInt32DataFieldAccessor(), expression);

        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, long>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new Int64DataFieldAccessor(), expression);

        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, ulong>> expression)
            where T : class
            => mapper.RegisterFieldAccessor(name, new UInt64DataFieldAccessor(), expression);

        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TimeSpan>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(
                name, 
                new Int64DataFieldAccessor().Transform(new TimeSpanToTicksConverter()), 
                expression);
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, float>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new SingleDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, double>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DoubleDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, decimal>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DecimalDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, string>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new StringDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, Guid>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new GuidDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, DateTime>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DateTimeDataFieldAccessor(), expression);
        
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, DateTimeOffset>> expression)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DateTimeDataFieldAccessor().Transform(new DateTimeOffsetToDateTimeConverter()), expression);
        public static void RegisterFieldAccessor<T>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, DateTimeOffset>> expression,
                DateTimeKind dateTimeKind)
            where T : class 
            => mapper.RegisterFieldAccessor(name, new DateTimeDataFieldAccessor().Transform(new DateTimeOffsetToDateTimeConverter(dateTimeKind)), expression);

        public static void RegisterEnumNameFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
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
        
        public static void RegisterEnumSByteValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
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
        
        public static void RegisterEnumByteValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
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
        
        public static void RegisterEnumInt16ValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
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
        
        public static void RegisterEnumUInt16ValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new UInt16DataFieldAccessor().Transform(new EnumToUInt16Converter<TEnum>()), 
                expression);
        }
        public static void RegisterEnumInt32ValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
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
        public static void RegisterEnumUInt32ValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new UInt32DataFieldAccessor().Transform(new EnumToUInt32Converter<TEnum>()), 
                expression);
        }
        public static void RegisterEnumInt64ValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
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
        public static void RegisterEnumUInt64ValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            mapper.RegisterFieldAccessor(
                name, 
                new UInt64DataFieldAccessor().Transform(new EnumToUInt64Converter<TEnum>()), 
                expression);
        }
        
        public static void RegisterEnumValueFieldAccessor<T, TEnum>(
                this DataRecordMapper<T> mapper, 
                string name, 
                Expression<Func<T, TEnum>> expression)
            where T: class
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            where TEnum: struct, IComparable, IConvertible, IFormattable
            #else
            where TEnum: struct, IComparable, IFormattable
            #endif
        {
            #if NETSTANDARD || NET45_OR_NEWER
            var typeCode = typeof(TEnum).GetTypeInfo().GetTypeCode();
            #else
            var typeCode = typeof(TEnum).GetTypeCode();
            #endif
            switch (typeCode)
            {
                case TypeCode.SByte:
                    RegisterEnumSByteValueFieldAccessor(mapper, name, expression);
                    break;
                case TypeCode.Byte:
                    RegisterEnumByteValueFieldAccessor(mapper, name, expression);
                    break;
                case TypeCode.Int16:
                    RegisterEnumInt16ValueFieldAccessor(mapper, name, expression);
                    break;
                case TypeCode.UInt16:
                    RegisterEnumUInt16ValueFieldAccessor(mapper, name, expression);
                    break;
                case TypeCode.Int32:
                    RegisterEnumInt32ValueFieldAccessor(mapper, name, expression);
                    break;
                case TypeCode.UInt32:
                    RegisterEnumUInt32ValueFieldAccessor(mapper, name, expression);
                    break;
                case TypeCode.Int64:
                    RegisterEnumInt64ValueFieldAccessor(mapper, name, expression);
                    break;
                case TypeCode.UInt64:
                    RegisterEnumUInt64ValueFieldAccessor(mapper, name, expression);
                    break;
                default:
                    throw new NotSupportedException(
                        $"Unable to determine the integral type behind enum type `{typeof(TEnum).FullName}` and type code {typeCode}.");
            }
        }
    }
}
#endif