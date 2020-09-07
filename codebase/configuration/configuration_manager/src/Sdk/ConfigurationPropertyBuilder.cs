#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Axle.Conversion.Parsing;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    [Serializable]
    public sealed class ConfigurationPropertyBuilder
	{
        public static ConfigurationPropertyBuilder Create(string name) => new ConfigurationPropertyBuilder(name);
	    public static ConfigurationPropertyBuilder Create(string name, Type type) => Create(name).OfType(type);

	    private readonly string _name;
	    private Type _type;
	    private object _defaultValue;
	    private ConfigurationPropertyOptions? _options;
	    private TypeConverter _typeConverter;
	    private ConfigurationValidatorBase _validator;
	    private string _description;

        public ConfigurationPropertyBuilder(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public ConfigurationPropertyBuilder OfType(Type type)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
            if (type.IsEnum && _typeConverter == null)
            {
                _typeConverter = (TypeConverter) Activator.CreateInstance(typeof(EnumNameConverter<>).MakeGenericType(type));
            }
            return this;
        }
        public ConfigurationPropertyBuilder OfType<T>() { return OfType(typeof(T)); }

        [Obsolete("Deprecated in favor of `UseDefaultValue<T>(T value)` method.")]
        public ConfigurationPropertyBuilder SetDefaultValueTo(object defaultValue)
        {
            _defaultValue = defaultValue;
            return this;
        }
        public ConfigurationPropertyBuilder UseDefaultValue<T>() => UseDefaultValue(default(T));

	    public ConfigurationPropertyBuilder UseDefaultValue<T>(T value)
        {
            _defaultValue = value;
            return this;
        }
        [Obsolete("Deprecated in favor of `UseDefaultValue<T>()` method.")]
        public ConfigurationPropertyBuilder WithDefaultValue<T>() => UseDefaultValue<T>();

	    public ConfigurationPropertyBuilder UsingOptions(ConfigurationPropertyOptions options)
        {
            SetFlag(options, true);
            return this;
        }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal ConfigurationPropertyBuilder SetFlag(ConfigurationPropertyOptions flag, bool? isSet)
        {
            if (isSet.HasValue)
            {
                if (!_options.HasValue && isSet.Value)
                {
                    _options = flag;
                }
                else
                {
                    if (isSet.Value)
                    {
                        _options |= flag;
                    }
                    else
                    {
                        _options &= ~flag;
                    }
                }
            }
            
            return this;
        }
        
        public ConfigurationPropertyBuilder IsKey() => IsKey(true);
	    public ConfigurationPropertyBuilder IsKey(bool isKey) => SetFlag(ConfigurationPropertyOptions.IsKey, isKey);

	    public ConfigurationPropertyBuilder IsRequired() => IsRequired(true);
	    public ConfigurationPropertyBuilder IsRequired(bool isRequired) => SetFlag(ConfigurationPropertyOptions.IsRequired, isRequired);

	    public ConfigurationPropertyBuilder ConvertWith(TypeConverter typeConverter)
        {
            _typeConverter = typeConverter;
            return this;
        }
        public ConfigurationPropertyBuilder ConvertWith<T>() where T: TypeConverter, new() { return ConvertWith(new T()); }        

        public ConfigurationPropertyBuilder ValidateWith(ConfigurationValidatorBase validator)
        {
            _validator = validator;
            return this;
        }
        public ConfigurationPropertyBuilder ValidateWith<T>() where T: ConfigurationValidatorBase, new() => ValidateWith(new T());

	    public ConfigurationPropertyBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public ConfigurationProperty Build()
        {
            return new ConfigurationProperty(
                _name,
                _type,
                _defaultValue,
                _typeConverter,
                _validator,
                _options ?? ConfigurationPropertyOptions.None,
                _description);
        }

	    public ConfigurationProperty[] BuildMany(int count) => Enumerable.ToArray(Enumerable.Select(Enumerable.Repeat(0, count), _ => Build()));

	    public static implicit operator ConfigurationProperty(ConfigurationPropertyBuilder builder) => builder.Build();
	}

    [Serializable]
    public sealed class ConfigurationPropertyBuilder<T>
    {
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private ConfigurationPropertyBuilder<T> Create(ConfigurationPropertyBuilder innerBuilder) => new ConfigurationPropertyBuilder<T>(innerBuilder);
        public static ConfigurationPropertyBuilder<T> Create(string name) => new ConfigurationPropertyBuilder<T>(name);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConfigurationPropertyBuilder _innerBuilder;

        public ConfigurationPropertyBuilder(string name) : this(new ConfigurationPropertyBuilder(name).OfType<T>()) { }
        internal ConfigurationPropertyBuilder(ConfigurationPropertyBuilder builder)
        {
            _innerBuilder = builder;
        }

        public ConfigurationPropertyBuilder<T> UseDefaultValue(T defaultValue)
        {
			_innerBuilder.UseDefaultValue(defaultValue);
            return this;
        }
        public ConfigurationPropertyBuilder<T> UseDefaultValue() => Create(_innerBuilder.UseDefaultValue<T>());

        public ConfigurationPropertyBuilder<T> UsingOptions(ConfigurationPropertyOptions options) => SetFlag(options, true);

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private ConfigurationPropertyBuilder<T> SetFlag(ConfigurationPropertyOptions flag, bool? set) => Create(_innerBuilder.SetFlag(flag, set));

        public ConfigurationPropertyBuilder<T> IsKey() => IsKey(true);
        public ConfigurationPropertyBuilder<T> IsKey(bool isKey) => SetFlag(ConfigurationPropertyOptions.IsKey, isKey);

        public ConfigurationPropertyBuilder<T> IsRequired() => IsRequired(true);
        public ConfigurationPropertyBuilder<T> IsRequired(bool isRequired) => SetFlag(ConfigurationPropertyOptions.IsRequired, isRequired);

        public ConfigurationPropertyBuilder<T> ConvertWith(TypeConverter typeConverter) => Create(_innerBuilder.ConvertWith(typeConverter));
        public ConfigurationPropertyBuilder<T> ConvertWith<TConverter>() where TConverter: TypeConverter, new()
        {
            return Create(_innerBuilder.ConvertWith<TConverter>());
        }
        public ConfigurationPropertyBuilder<T> ParseWith<TParser>() where TParser: IParser<T>, new() => ConvertWith(new GenericConverter<T, TParser>());

        public ConfigurationPropertyBuilder<T> ValidateWith(ConfigurationValidatorBase validator) => Create(_innerBuilder.ValidateWith(validator));
        public ConfigurationPropertyBuilder<T> ValidateWith<TVal>() where TVal: ConfigurationValidatorBase, new() => ValidateWith(new TVal());

        public ConfigurationPropertyBuilder<T> DescribeAs(string description) => Create(_innerBuilder.SetDescription(description));

        public ConfigurationProperty Build() => _innerBuilder.Build();
        public ConfigurationProperty[] BuildMany(int count) => _innerBuilder.BuildMany(count);

        public static implicit operator ConfigurationProperty(ConfigurationPropertyBuilder<T> builder) => builder.Build();
    }
}
#endif