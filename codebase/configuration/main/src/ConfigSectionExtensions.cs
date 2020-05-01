#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Axle.Text.Documents.Binding;
using Axle.Verification;

namespace Axle.Configuration
{
    /// <summary>
    /// A static class containing common configuration extension methods.
    /// </summary>
    public static class ConfigSectionExtensions
    {
        private sealed class ConfigurationBindingCollectionProvider : IBoundCollectionProvider
        {
            private readonly IEnumerable<IBoundValueProvider> _valueProviders;
            
            public ConfigurationBindingCollectionProvider(string name, IEnumerable<IBoundValueProvider> valueProviders)
            {
                Name = name;
                _valueProviders = valueProviders;
            }

            public IEnumerator<IBoundValueProvider> GetEnumerator() => _valueProviders.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

            public IBindingCollectionAdapter CollectionAdapter => null;
            public string Name { get; }
        }
        private sealed class ConfigurationBindingValueProvider : IBoundComplexValueProvider
        {
            public static IBoundValueProvider Get(IConfigSetting setting)
            {
                switch (setting)
                {
                    case null:
                        return null;
                    case IConfigSection section:
                        return new ConfigurationBindingValueProvider(section);
                    case IConfigSetting _:
                        return new BoundSimpleValueProvider(string.Empty, setting.Value);
                }
            }
            private readonly IConfigSection _config;

            public ConfigurationBindingValueProvider(IConfigSection config)
            {
                _config = config;
            }

            public bool TryGetValue(string member, out IBoundValueProvider value) 
                => (value = this[member]) != null;

            public IBoundValueProvider this[string member]
            {
                get
                {
                    var settings = _config[member].ToList();
                    if (settings.Count == 0)
                    {
                        var cmp = StringComparer.OrdinalIgnoreCase;
                        settings = _config.Keys
                            .Where(x => cmp.Equals(x, member))
                            .SelectMany(x => _config[x])
                            .ToList();
                    }

                    return new ConfigurationBindingCollectionProvider(member, settings.Select(Get));
                }
            }
        
            public string Name => _config.Name;
        }

        private sealed class IncludeExcludeElementCollection<T> : IIncludeExcludeElementCollection<T>
        {
            public IncludeExcludeElementCollection(IEnumerable<T> includeElements, IEnumerable<T> excludeElements)
            {
                IncludeElements = includeElements;
                ExcludeElements = excludeElements;
            }

            public IEnumerable<T> IncludeElements { get; }
            public IEnumerable<T> ExcludeElements { get; }
        }

        private const string IncludeExcludeElementCollectionIncludeSection = "include";
        private const string IncludeExcludeElementCollectionExcludeSection = "exclude";
        
        private static IConfigSetting GetSetting(this IConfigSection config, string sectionName)
        {
            return string.IsNullOrEmpty(sectionName) ? config : config[sectionName].FirstOrDefault();
        }
        
        public static IConfigSection GetSection(this IConfigSection config, string sectionName) 
            => GetSections(config, sectionName).OfType<IConfigSection>().FirstOrDefault();

        public static object GetSection(this IConfigSection config, string sectionName, Type sectionType)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(config, nameof(config)));
            var configSection = GetSetting(config, sectionName);
            return configSection != null 
                ? new DefaultBinder().Bind(ConfigurationBindingValueProvider.Get(configSection), sectionType)
                : null;
        }

        public static T GetSection<T>(this IConfigSection config, string sectionName) where T: class, new()
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(config, nameof(config)));
            var configSection = GetSetting(config, sectionName);
            return configSection != null
                ? (T) new DefaultBinder().Bind(ConfigurationBindingValueProvider.Get(configSection), new T())
                : null;
        }
        
        public static IEnumerable<IConfigSetting> GetSections(this IConfigSection config, string sectionName)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(config, nameof(config)));
            return string.IsNullOrEmpty(sectionName) ? new[]{config} : config[sectionName];
        }
        
        public static IEnumerable<object> GetSections(this IConfigSection config, string sectionName, Type sectionType)
        {
            return GetSections(config, sectionName).Select<IConfigSetting, object>(
                configSection =>
                    new DefaultBinder().Bind(ConfigurationBindingValueProvider.Get(configSection), sectionType));
        }

        public static IEnumerable<T> GetSections<T>(this IConfigSection config, string sectionName) where T: class, new()
        {
            return GetSections(config, sectionName)
                .Select<IConfigSetting, object>(configSection => new DefaultBinder().Bind(ConfigurationBindingValueProvider.Get(configSection), new T()))
                .Cast<T>();
        }

        public static IIncludeExcludeElementCollection<T> GetIncludeExcludeCollection<T>(this IConfigSection config, string sectionName)
        {
            var sections = GetSections(config, sectionName).OfType<IConfigSection>().ToList();
            var includeElements = sections.SelectMany(section => GetSections(section, IncludeExcludeElementCollectionIncludeSection, typeof(T)).Cast<T>());
            var excludeElements = sections.SelectMany(section => GetSections(section, IncludeExcludeElementCollectionExcludeSection, typeof(T)).Cast<T>());
            return new IncludeExcludeElementCollection<T>(includeElements, excludeElements);
        }
    }
}
#endif
