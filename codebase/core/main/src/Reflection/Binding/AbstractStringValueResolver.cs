#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Axle.Conversion.Parsing;
using Axle.Verification;

namespace Axle.Reflection.Binding
{
    public abstract class AbstractStringValueResolver : IBindingResolver
    {
        private readonly IDictionary<Type, IParser> _parsers = new Dictionary<Type, IParser>();

        private AbstractStringValueResolver()
        {
            _parsers.Add(typeof(bool), new BooleanParser());
            _parsers.Add(typeof(char), new CharacterParser());
            _parsers.Add(typeof(sbyte), new SByteParser());
            _parsers.Add(typeof(byte), new ByteParser());
            _parsers.Add(typeof(short), new Int16Parser());
            _parsers.Add(typeof(int), new Int32Parser());
            _parsers.Add(typeof(long), new Int64Parser());
            _parsers.Add(typeof(ushort), new UInt16Parser());
            _parsers.Add(typeof(uint), new UInt32Parser());
            _parsers.Add(typeof(ulong), new UInt64Parser());
            _parsers.Add(typeof(float), new SingleParser());
            _parsers.Add(typeof(double), new DoubleParser());
            _parsers.Add(typeof(decimal), new DecimalParser());
            _parsers.Add(typeof(Guid), new GuidParser());
            _parsers.Add(typeof(Version), new VersionParser());
            _parsers.Add(typeof(Assembly), new AssemblyParser());
            _parsers.Add(typeof(Uri), new Axle.Conversion.Parsing.UriParser());
        }
        protected AbstractStringValueResolver(string memberName, CultureInfo culture) : this()
        {
            MemberName = StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(memberName, nameof(memberName)));
            Parent = null;
            Culture = Verifier.IsNotNull(Verifier.VerifyArgument(culture, nameof(culture)));
        }
        protected AbstractStringValueResolver(string memberName, AbstractStringValueResolver parent) : this()
        {
            MemberName = StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(memberName, nameof(memberName)));
            Parent = Verifier.IsNotNull(Verifier.VerifyArgument(parent, nameof(parent)));
            Culture = parent.Culture;
        }

        protected abstract bool TryResolveRawValue(string memberName, out string rawValue);

        protected virtual bool TryConvert(Type type, string rawValue, out object result)
        {
            result = null;
            return _parsers.TryGetValue(type, out var parser) && parser.TryParse(rawValue, Culture, out result);
        }

        protected abstract AbstractStringValueResolver CreateNestedResolver(string memberName);

        bool IBindingResolver.TryResolve(Type type, string memberName, out object resolvedValue)
        {
            resolvedValue = null;
            return TryResolveRawValue(memberName, out var rawValue) && TryConvert(type, rawValue, out resolvedValue);
        }

        IBindingResolver IBindingResolver.CreateNestedResolver(string memberName) => CreateNestedResolver(memberName);
        IBindingResolver IBindingResolver.Parent => Parent;

        public AbstractStringValueResolver Parent { get; }
        public CultureInfo Culture { get; }
        public string MemberName { get; }
    }
}
#endif