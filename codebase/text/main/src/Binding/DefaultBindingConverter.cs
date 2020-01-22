using Axle.Conversion;
using Axle.Conversion.Parsing;
using Axle.Verification;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Axle.Text.Data.Binding
{
    /// <summary>
    /// A <see cref="IBindingConverter"/> implementation supporting the conversion of a handful of primitive types. 
    /// </summary>
    public sealed class DefaultBindingConverter : IBindingConverter
    {
        private static readonly IDictionary<Type, IConverter<string, object>> _fallbackConverters;

        static DefaultBindingConverter() 
        {
            var booleanParser = new BooleanParser();
            var characterParser = new CharacterParser();
            var sbyteParser = new SByteParser();
            var byteParser = new ByteParser();
            var int16Parser = new Int16Parser();
            var uInt16Parser = new UInt16Parser();
            var int32Parser = new Int32Parser();
            var uInt32Parser = new UInt32Parser();
            var int64Parser = new Int64Parser();
            var uInt64Parser = new UInt64Parser();
            var singleParser = new SingleParser();
            var doubleParser = new DoubleParser();
            var decimalParser = new DecimalParser();
            var dateTimeParser = new DateTimeISOParser();
            var dateTimeOffsetParser = new DateTimeOffsetParser();
            var timeSpanParser = new TimeSpanParser();
            var guidParser = new GuidParser();

            _fallbackConverters = new Dictionary<Type, IConverter<string, object>>
            {
                { typeof(bool),             new BoxingConverter<bool>(booleanParser) },
                { typeof(char),             new BoxingConverter<char>(characterParser) },
                { typeof(sbyte),            new BoxingConverter<sbyte>(sbyteParser) },
                { typeof(byte),             new BoxingConverter<byte>(byteParser) },
                { typeof(short),            new BoxingConverter<short>(int16Parser) },
                { typeof(ushort),           new BoxingConverter<ushort>(uInt16Parser) },
                { typeof(int),              new BoxingConverter<int>(int32Parser) },
                { typeof(uint),             new BoxingConverter<uint>(uInt32Parser) },
                { typeof(long),             new BoxingConverter<long>(int64Parser) },
                { typeof(ulong),            new BoxingConverter<ulong>(uInt64Parser) },
                { typeof(float),            new BoxingConverter<float>(singleParser) },
                { typeof(double),           new BoxingConverter<double>(doubleParser) },
                { typeof(decimal),          new BoxingConverter<decimal>(decimalParser) },
                { typeof(string),           new BoxingConverter<string>(new IdentityConverter<string>()) },
                { typeof(DateTime),         new BoxingConverter<DateTime>(dateTimeParser) },
                { typeof(DateTimeOffset),   new BoxingConverter<DateTimeOffset>(dateTimeOffsetParser) },
                { typeof(TimeSpan),         new BoxingConverter<TimeSpan>(timeSpanParser) },
                { typeof(Guid),             new BoxingConverter<Guid>(guidParser) },
                { typeof(Version),          new BoxingConverter<Version>(new VersionParser()) },
                { typeof(Uri),              new BoxingConverter<Uri>(new Axle.Conversion.Parsing.UriParser()) },
                { typeof(Assembly),         new BoxingConverter<Assembly>(new AssemblyParser()) },
                #if NETSTANDARD || NET35_OR_NEWER
                { typeof(bool?),            new BoxingConverter<bool?>(booleanParser.GetNullableParser()) },
                { typeof(char?),            new BoxingConverter<char?>(characterParser.GetNullableParser()) },
                { typeof(sbyte?),           new BoxingConverter<sbyte?>(sbyteParser.GetNullableParser()) },
                { typeof(byte?),            new BoxingConverter<byte?>(byteParser.GetNullableParser()) },
                { typeof(short?),           new BoxingConverter<short?>(int16Parser.GetNullableParser()) },
                { typeof(ushort?),          new BoxingConverter<ushort?>(uInt16Parser.GetNullableParser()) },
                { typeof(int?),             new BoxingConverter<int?>(int32Parser.GetNullableParser()) },
                { typeof(uint?),            new BoxingConverter<uint?>(uInt32Parser.GetNullableParser()) },
                { typeof(long?),            new BoxingConverter<long?>(int64Parser.GetNullableParser()) },
                { typeof(ulong?),           new BoxingConverter<ulong?>(uInt64Parser.GetNullableParser()) },
                { typeof(float?),           new BoxingConverter<float?>(singleParser.GetNullableParser()) },
                { typeof(double?),          new BoxingConverter<double?>(doubleParser.GetNullableParser()) },
                { typeof(decimal?),         new BoxingConverter<decimal?>(decimalParser.GetNullableParser()) },
                { typeof(DateTime?),        new BoxingConverter<DateTime?>(dateTimeParser.GetNullableParser()) },
                { typeof(DateTimeOffset?),  new BoxingConverter<DateTimeOffset?>(dateTimeOffsetParser.GetNullableParser()) },
                { typeof(TimeSpan?),        new BoxingConverter<TimeSpan?>(timeSpanParser.GetNullableParser()) },
                { typeof(Guid?),            new BoxingConverter<Guid?>(guidParser.GetNullableParser()) },
                #endif
            };
        }

        /// <inheritdoc/>
        public bool TryConvertMemberValue(string rawValue, Type targetType, out object boundValue)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(targetType, nameof(targetType)));
            boundValue = null;
            return _fallbackConverters.TryGetValue(targetType, out var converter) && converter.TryConvert(rawValue, out boundValue);
        }
    }
}
