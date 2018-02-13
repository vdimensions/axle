using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Axle.Extensions.String;
using Axle.Resources.Extraction;
using Axle.Verification;


namespace Axle.Resources.Java.Extraction
{
	using JavaProperties = Kajabity.Tools.Java.JavaProperties;

    internal sealed class JavaPropertiesResourceExtractor : ResourceExtractorChain
    {
        const string PropertiesExt = ".properties";

        private void GetPropertiesFileData(Uri baseUri, out Uri _propertiesFileLocation, out string propertyFileName, out string _propertyPrefix)
        {
            var location = baseUri;

            var fragments = location.ToString().Split(StringSplitOptions.None, '/');
            if (fragments.Length < 1)
            {
                throw new InvalidOperationException("Invalid properties file location");
            }
            string propertiesName = null;
            var propertyFileLocationBuilder = new StringBuilder(fragments[0]);
            var locationRemainderBuilder = new StringBuilder();
            var sb = propertyFileLocationBuilder;
            for (var i = 1; i < fragments.Length; i++)
            {
                sb.Append("/");
                var fragment = fragments[i];
                if (propertiesName == null && fragment.EndsWith(PropertiesExt, StringComparison.OrdinalIgnoreCase))
                {
                    propertiesName = fragment;
                    sb = locationRemainderBuilder;
                    continue;
                }
                sb.Append(fragment);
            }
            if (propertiesName == null)
            {
                throw new InvalidOperationException("Invalid properties file location");
            }

            _propertiesFileLocation = new Axle.Conversion.Parsing.UriParser().Parse(propertyFileLocationBuilder.ToString());
            propertyFileName = propertiesName;
            _propertyPrefix = locationRemainderBuilder.ToString();
        }

        public override bool TryExtract(Uri location, string name, CultureInfo culture, IResourceExtractor nextInChain, out ResourceInfo resource)
        {
            resource = null;
            GetPropertiesFileData(location, out var _propertiesFileUri, out var _propertiesFileName, out var _propertyPrefix);

            culture.VerifyArgument(nameof(culture)).IsNotNull();
            if (!nextInChain.TryExtract(_propertiesFileUri, _propertiesFileName, culture, out var propertiesResource))
            {
                return false;
            }

            var stream = propertiesResource.Open();
            if (stream == null)
            {
                return false;
            }

            var utf8 = Encoding.UTF8;
            try
            {
                var propertiesFile = new JavaProperties();
                using (stream)
                {
                    propertiesFile.Load(stream, utf8);
                }

                var prop = new Dictionary<string, object>(StringComparer.Ordinal);
                foreach (string key in propertiesFile.Keys)
                {
                    prop[key] = propertiesFile.GetProperty(key);
                }

                var propertyResourceKey = $"{_propertyPrefix}{name}";
                if (!prop.ContainsKey(propertyResourceKey))
                {
                    return false;
                }
                resource = new TextResourceInfo(new Uri(_propertiesFileName, UriKind.RelativeOrAbsolute), name, culture, prop[propertyResourceKey].ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}