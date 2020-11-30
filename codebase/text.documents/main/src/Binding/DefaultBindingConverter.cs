using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Security;
using Axle.Conversion;
using Axle.Text.Parsing;
using Axle.Verification;

namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// A <see cref="IBindingConverter"/> implementation supporting the conversion of a handful of primitive types. 
    /// </summary>
    public sealed class DefaultBindingConverter : IBindingConverter
    {
        private sealed class InvariantParser<T> : DelegatingConverter<CharSequence, T>
        {
            public static IConverter<CharSequence, T> Create(IParser<T> parser) => new InvariantParser<T>(parser);
            
            private readonly IParser<T> _parser;

            private InvariantParser(IParser<T> parser) : base(parser)
            {
                _parser = parser;
            }

            public override T Convert(CharSequence source)
            {
                return _parser.Parse(source, CultureInfo.InvariantCulture);
            }

            public override bool TryConvert(CharSequence source, out T target)
            {
                return _parser.TryParse(source, CultureInfo.InvariantCulture, out target);
            }
        }

        private static IConverter<CharSequence, T> CultureInvariant<T>(IParser<T> parser)
            where T: IFormattable
        {
            return InvariantParser<T>.Create(parser);
        }

        private static readonly IDictionary<Type, IConverter<CharSequence, object>> _converters;

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

            _converters = new Dictionary<Type, IConverter<CharSequence, object>>
            {
                { typeof(bool),             new BoxingConverter<bool>(booleanParser) },
                { typeof(char),             new BoxingConverter<char>(characterParser) },
                { typeof(sbyte),            new BoxingConverter<sbyte>(CultureInvariant(sbyteParser)) },
                { typeof(byte),             new BoxingConverter<byte>(CultureInvariant(byteParser)) },
                { typeof(short),            new BoxingConverter<short>(CultureInvariant(int16Parser)) },
                { typeof(ushort),           new BoxingConverter<ushort>(CultureInvariant(uInt16Parser)) },
                { typeof(int),              new BoxingConverter<int>(CultureInvariant(int32Parser)) },
                { typeof(uint),             new BoxingConverter<uint>(CultureInvariant(uInt32Parser)) },
                { typeof(long),             new BoxingConverter<long>(CultureInvariant(int64Parser)) },
                { typeof(ulong),            new BoxingConverter<ulong>(CultureInvariant(uInt64Parser)) },
                { typeof(float),            new BoxingConverter<float>(CultureInvariant(singleParser)) },
                { typeof(double),           new BoxingConverter<double>(CultureInvariant(doubleParser)) },
                { typeof(decimal),          new BoxingConverter<decimal>(CultureInvariant(decimalParser)) },
                { typeof(string),           new BoxingConverter<string>(new StringToCharSequenceConverter().Invert()) },
                { typeof(DateTime),         new BoxingConverter<DateTime>(CultureInvariant(dateTimeParser)) },
                { typeof(DateTimeOffset),   new BoxingConverter<DateTimeOffset>(CultureInvariant(dateTimeOffsetParser)) },
                #if !NETSTANDARD && !NET40_OR_NEWER
                // Strangely enough, TimeSpan seems to not implement IFormattable before net40 
                { typeof(TimeSpan),         new BoxingConverter<TimeSpan>(InvariantParser<TimeSpan>.Create(timeSpanParser)) },
                #else
                { typeof(TimeSpan),         new BoxingConverter<TimeSpan>(CultureInvariant(timeSpanParser)) },
                #endif
                { typeof(Guid),             new BoxingConverter<Guid>(CultureInvariant(guidParser)) },
                #if NETSTANDARD || NET35_OR_NEWER
                { typeof(bool?),            new BoxingConverter<bool?>(booleanParser.GetNullableParser()) },
                { typeof(char?),            new BoxingConverter<char?>(characterParser.GetNullableParser()) },
                { typeof(sbyte?),           new BoxingConverter<sbyte?>(InvariantParser<sbyte?>.Create(sbyteParser.GetNullableParser())) },
                { typeof(byte?),            new BoxingConverter<byte?>(InvariantParser<byte?>.Create(byteParser.GetNullableParser())) },
                { typeof(short?),           new BoxingConverter<short?>(InvariantParser<short?>.Create(int16Parser.GetNullableParser())) },
                { typeof(ushort?),          new BoxingConverter<ushort?>(InvariantParser<ushort?>.Create(uInt16Parser.GetNullableParser())) },
                { typeof(int?),             new BoxingConverter<int?>(InvariantParser<int?>.Create(int32Parser.GetNullableParser())) },
                { typeof(uint?),            new BoxingConverter<uint?>(InvariantParser<uint?>.Create(uInt32Parser.GetNullableParser())) },
                { typeof(long?),            new BoxingConverter<long?>(InvariantParser<long?>.Create(int64Parser.GetNullableParser())) },
                { typeof(ulong?),           new BoxingConverter<ulong?>(InvariantParser<ulong?>.Create(uInt64Parser.GetNullableParser())) },
                { typeof(float?),           new BoxingConverter<float?>(InvariantParser<float?>.Create(singleParser.GetNullableParser())) },
                { typeof(double?),          new BoxingConverter<double?>(InvariantParser<double?>.Create(doubleParser.GetNullableParser())) },
                { typeof(decimal?),         new BoxingConverter<decimal?>(InvariantParser<decimal?>.Create(decimalParser.GetNullableParser())) },
                { typeof(DateTime?),        new BoxingConverter<DateTime?>(InvariantParser<DateTime?>.Create(dateTimeParser.GetNullableParser())) },
                { typeof(DateTimeOffset?),  new BoxingConverter<DateTimeOffset?>(InvariantParser<DateTimeOffset?>.Create(dateTimeOffsetParser.GetNullableParser())) },
                { typeof(TimeSpan?),        new BoxingConverter<TimeSpan?>(InvariantParser<TimeSpan?>.Create(timeSpanParser.GetNullableParser())) },
                { typeof(Guid?),            new BoxingConverter<Guid?>(InvariantParser<Guid?>.Create(guidParser.GetNullableParser())) },
                #endif
                { typeof(Uri),              new BoxingConverter<Uri>(new Axle.Text.Parsing.UriParser()) },
                { typeof(Type),             new BoxingConverter<Type>(new TypeParser()) },
                { typeof(Assembly),         new BoxingConverter<Assembly>(new AssemblyParser()) },
                { typeof(Version),          new BoxingConverter<Version>(new VersionParser()) },
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                { typeof(CultureInfo),      new BoxingConverter<CultureInfo>(new CultureInfoParser()) },
                { typeof(SecureString),     new BoxingConverter<SecureString>(new SecureStringParser()) },
                #endif
            };
        }

        /// <inheritdoc/>
        public bool TryConvertMemberValue(CharSequence rawValue, Type targetType, out object boundValue)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(targetType, nameof(targetType)));
            boundValue = null;
            return _converters.TryGetValue(targetType, out var converter) && converter.TryConvert(rawValue, out boundValue);
        }
    }
}
