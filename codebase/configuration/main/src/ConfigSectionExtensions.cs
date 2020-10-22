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
        private sealed class ConfigurationBindingCollectionProvider : IDocumentCollectionProvider
        {
            private readonly IEnumerable<IDocumentValueProvider> _valueProviders;
            
            public ConfigurationBindingCollectionProvider(string name, IEnumerable<IDocumentValueProvider> valueProviders)
            {
                Name = name;
                _valueProviders = valueProviders;
            }

            public IEnumerator<IDocumentValueProvider> GetEnumerator() => _valueProviders.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

            public IDocumentCollectionValueAdapter CollectionValueAdapter => null;
            public string Name { get; }
        }
        
        private sealed class ConfigurationBindingValueProvider : IDocumentComplexValueProvider
        {
            public static IDocumentValueProvider Get(IConfigSetting setting)
            {
                switch (setting)
                {
                    case null:
                        return null;
                    case IConfigSection section:
                        return new ConfigurationBindingValueProvider(section);
                    case IConfigSetting _:
                        return new DocumentSimpleValueProvider(string.Empty, setting.Value);
                }
            }
            private readonly IConfigSection _config;

            private ConfigurationBindingValueProvider(IConfigSection config)
            {
                _config = config;
            }

            public bool TryGetValue(string member, out IDocumentValueProvider value) 
                => (value = this[member]) != null;

            public IDocumentValueProvider this[string member]
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
        
        private static IConfigSetting GetSetting(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IConfigSection config, string sectionName)
        {
            return string.IsNullOrEmpty(sectionName) ? config : config[sectionName].FirstOrDefault();
        }
        
        /// <summary>
        /// Obtains the <see cref="IConfigSection">configuration section</see> child of a given
        /// <paramref name="config"/> object that corresponds to the specified section <paramref name="name"/>.
        /// </summary>
        /// <param name="config">
        /// The <see cref="IConfigSection"/> instance to obtain the section from.
        /// </param>
        /// <param name="name">
        /// The name of the section to obtain.
        /// </param>
        /// <returns>
        /// A <see cref="IConfigSection"/> that corresponds to the configuration section child of the current
        /// <paramref name="config"/> with the specified <paramref name="name"/>, or <c>null</c> if a configuration
        /// section with that name does not exist.
        /// </returns>
        public static IConfigSection GetSection(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IConfigSection config, string name) 
            => Enumerable.FirstOrDefault(Enumerable.OfType<IConfigSection>(GetSections(config, name)));

        /// <summary>
        /// Obtains a strongly-typed representation for the <see cref="IConfigSection">configuration section</see> child
        /// of a given <paramref name="config"/> object that corresponds to the specified section
        /// <paramref name="name"/>.
        /// </summary>
        /// <param name="config">
        /// The <see cref="IConfigSection"/> instance to obtain the section from.
        /// </param>
        /// <param name="name">
        /// The name of the section to obtain.
        /// </param>
        /// <param name="sectionType">
        /// The type of the object representing the requested configuration section.
        /// </param>
        /// <returns>
        /// An instance of the <paramref name="sectionType"/> that corresponds to the configuration section child of the
        /// current <paramref name="config"/> with the specified <paramref name="name"/>, or <c>null</c> if a
        /// configuration section with that name does not exist.
        /// </returns>
        public static object GetSection(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IConfigSection config, string name, Type sectionType)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(config, nameof(config)));
            var configSection = GetSetting(config, name);
            return configSection != null 
                ? new DefaultDocumentBinder().Bind(ConfigurationBindingValueProvider.Get(configSection), sectionType)
                : null;
        }

        /// <summary>
        /// Obtains a strongly-typed representation for the <see cref="IConfigSection">configuration section</see> child
        /// of a given <paramref name="config"/> object that corresponds to the specified section
        /// <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object representing the requested configuration section.
        /// It must be a reference type, and must have a public default constructor.  
        /// </typeparam>
        /// <param name="config">
        /// The <see cref="IConfigSection"/> instance to obtain the section from.
        /// </param>
        /// <param name="name">
        /// The name of the section to obtain.
        /// </param>
        /// <returns>
        /// An instance of the <typeparamref name="T"/> that corresponds to the configuration section child of the
        /// current <paramref name="config"/> with the specified <paramref name="name"/>, or <c>null</c> if a
        /// configuration section with that name does not exist.
        /// </returns>
        public static T GetSection<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this 
            #endif
            IConfigSection config, string name) where T: class, new()
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(config, nameof(config)));
            var configSection = GetSetting(config, name);
            return configSection != null
                ? (T) new DefaultDocumentBinder().Bind(ConfigurationBindingValueProvider.Get(configSection), new T())
                : null;
        }
        
        public static IEnumerable<IConfigSetting> GetSections(
            #if NETSTANDARD || NET35_OR_NEWER
            this 
            #endif
            IConfigSection config, string sectionName)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(config, nameof(config)));
            return string.IsNullOrEmpty(sectionName) ? new[]{config} : config[sectionName];
        }
        
        public static IEnumerable<object> GetSections(
            #if NETSTANDARD || NET35_OR_NEWER
            this 
            #endif
            IConfigSection config, string sectionName, Type sectionType)
        {
            return Enumerable.Select(
                GetSections(config, sectionName), 
                configSection => 
                    new DefaultDocumentBinder().Bind(ConfigurationBindingValueProvider.Get(configSection), sectionType));
        }

        public static IEnumerable<T> GetSections<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this 
            #endif
            IConfigSection config, string sectionName) where T: class, new()
        {
            return Enumerable.Cast<T>(
                Enumerable.Select(
                    GetSections(config, sectionName), 
                    configSection => 
                        new DefaultDocumentBinder().Bind(ConfigurationBindingValueProvider.Get(configSection), new T())));
        }

        public static IIncludeExcludeElementCollection<T> GetIncludeExcludeCollection<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this 
            #endif
            IConfigSection config, string sectionName)
        {
            var sections = Enumerable.ToList(Enumerable.OfType<IConfigSection>(GetSections(config, sectionName)));
            var includeElements = Enumerable.SelectMany(
                sections, 
                section => Enumerable.Cast<T>(
                    GetSections(section, IncludeExcludeElementCollectionIncludeSection, typeof(T))));
            var excludeElements = Enumerable.SelectMany(
                sections, 
                section => Enumerable.Cast<T>(
                    GetSections(section, IncludeExcludeElementCollectionExcludeSection, typeof(T))));
            return new IncludeExcludeElementCollection<T>(includeElements, excludeElements);
        }
    }
}
#endif
