#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;
using System.Diagnostics;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    public class IncludeExcludeElementCollection<T, TIncludeCollection, TExcludeCollection> : 
            AbstractConfigurationElement, 
            IIncludeExcludeElementCollection<T, TIncludeCollection, TExcludeCollection>
        where T: ConfigurationElement
        where TIncludeCollection: ConfigurationElementCollection, IConfigurationElementCollection<T>, new()
        where TExcludeCollection: ConfigurationElementCollection, IConfigurationElementCollection<T>, new()
    {
        private const string IncludeElementTagName = "include";
        private const string ExcludeElementTagName = "exclude";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly ConfigurationProperty _includeElements = CreateProperty<TIncludeCollection>(IncludeElementTagName).UseDefaultValue(new TIncludeCollection());
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly ConfigurationProperty _excludeElements = CreateProperty<TExcludeCollection>(ExcludeElementTagName).UseDefaultValue(new TExcludeCollection());
        
        protected override void RegisterProperties(ConfigurationPropertyCollection properties)
        {
            base.RegisterProperties(properties);
            properties.Add(_includeElements);
            properties.Add(_excludeElements);
        }

        public TIncludeCollection IncludeElements => Resolve<TIncludeCollection>(_includeElements);
        public TExcludeCollection ExcludeElements => Resolve<TExcludeCollection>(_excludeElements);
    }
    public class IncludeExcludeElementCollection<T, TCollection> : IncludeExcludeElementCollection<T, TCollection, TCollection> 
        where T: ConfigurationElement
        where TCollection: ConfigurationElementCollection, IConfigurationElementCollection<T>, new() { }
}
#endif
