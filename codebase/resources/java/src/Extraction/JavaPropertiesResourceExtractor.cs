using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Axle.Extensions.String;
using Axle.Resources.Extraction;
using Axle.Verification;


namespace Axle.Resources.Java.Extraction
{
	using JavaProperties = global::Kajabity.Tools.Java.JavaProperties;

    internal sealed class JavaPropertiesResourceExtractor : ResourceExtractorChain
    {
        const string PropertiesExt = ".properties";

        private readonly string _propertiesFileName;
        private readonly string _propertyPrefix;

        public JavaPropertiesResourceExtractor(Uri baseUri)
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

            _propertiesFileName = propertiesName;
            _propertyPrefix = locationRemainderBuilder.ToString();
        }

        public override ResourceInfo Extract(string name, CultureInfo culture, IResourceExtractor nextInChain)
        {
            if (name.Equals(_propertiesFileName, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            culture.VerifyArgument(nameof(culture)).IsNotNull();
            var properties = new[]{ nextInChain.Extract($"{_propertiesFileName}", culture)}
                .Select(
                    x =>
                    {
                        var stream = x.Open();
                        if (stream == null)
                        {
                            return null;
                        }

                        var utf8 = Encoding.UTF8;
                        try
                        {
                            var propertiesFile = new JavaProperties();
                            using (stream)
                            {
                                propertiesFile.Load(stream, utf8);
                            }
                            return propertiesFile;
                        }
                        catch
                        {
                            return null;
                        }
                    })
                .Where(x => x != null);
            var prop = properties.Reverse().Aggregate(
                new Dictionary<string, object>(StringComparer.Ordinal),
                (agg, x) =>
                {
                    foreach (string key in x.Keys)
                    {
                        agg[key] = x.GetProperty(key);
                    }
                    return agg;
                });
            var propertyResourceKey = $"{_propertyPrefix}{name}";
            if (!prop.ContainsKey(propertyResourceKey))
            {
                return null;
            }
            return new TextResourceInfo(new Uri(_propertiesFileName, UriKind.RelativeOrAbsolute), name, culture, prop[propertyResourceKey].ToString());
        }
    }
}