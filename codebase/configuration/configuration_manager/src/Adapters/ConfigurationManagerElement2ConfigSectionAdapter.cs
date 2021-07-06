#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Configuration;
using Axle.Reflection.Extensions.Type;

namespace Axle.Configuration.ConfigurationManager.Adapters
{
    internal sealed class ConfigurationManagerElement2ConfigSectionAdapter : AbstractConfigurationManagerSectionAdapter
    {
        public ConfigurationManagerElement2ConfigSectionAdapter(ConfigurationElement section, string name) : base(name)
        {
            foreach (PropertyInformation propertyInfo in section.ElementInformation.Properties)
            {
                var propertyName = propertyInfo.Name;
                if (string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }
                var value = propertyInfo.Value;
                if (value == null)
                {
                    continue;
                }
                string strValue = null;
                try
                {
                    strValue = propertyInfo.Converter.ConvertToString(value);
                }
                catch
                {
                    if (TypeExtensions.GetTypeCode(value.GetType()) != TypeCode.Object)
                    {
                        strValue = value.ToString();
                    }
                }
                if (strValue != null)
                {
                    Properties.Add(propertyName, ConfigSetting.Create(strValue));
                }                    
            }
        }
    }
}
#endif