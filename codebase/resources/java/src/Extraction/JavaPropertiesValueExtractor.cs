using System;
using System.Collections.Generic;
using System.Text;

using Axle.Resources.Extraction;
using Axle.Extensions.String;

using Kajabity.Tools.Java;


namespace Axle.Resources.Java.Extraction
{
    public sealed class JavaPropertiesValueExtractor : ResourceExtractorChain
    {
        private const string PropertiesExt = ".properties";

        public JavaPropertiesValueExtractor() : base() { }

        private bool GetPropertiesFileData(Uri location, out string propertyFileName, out string keyPrefix)
        {
            propertyFileName = keyPrefix = null;

            var locStr = location.ToString();
            keyPrefix = locStr.TakeAfterFirst(PropertiesExt);
            propertyFileName = locStr.TakeBeforeFirst(keyPrefix);

            return !string.IsNullOrEmpty(propertyFileName) && propertyFileName.EndsWith(PropertiesExt, StringComparison.OrdinalIgnoreCase);

            //var fragments = location.ToString().Split(StringSplitOptions.None, '/');
            //if (fragments.Length < 1)
            //{
            //    return false;
            //}
            //
            //
            //
            //string propertiesName = null;
            //var propertyFileLocationBuilder = new StringBuilder(fragments[0]);
            //var locationRemainderBuilder = new StringBuilder();
            //var sb = propertyFileLocationBuilder;
            //for (var i = 1; i < fragments.Length; i++)
            //{
            //    sb.Append("/");
            //    var fragment = fragments[i];
            //    if (propertiesName == null && fragment.EndsWith(PropertiesExt, StringComparison.OrdinalIgnoreCase))
            //    {
            //        propertiesName = fragment;
            //        sb = locationRemainderBuilder;
            //        continue;
            //    }
            //    sb.Append(fragment);
            //}
            //if (propertiesName == null)
            //{
            //    return false;
            //}
            //
            //keyPrefix = locationRemainderBuilder.ToString();
            //return true;
        }

        public override ResourceInfo Extract(ResourceContext context, string name, IResourceExtractor nextInChain)
        {
            foreach (var location in context.LookupLocations)
            {
                if (GetPropertiesFileData(location, out var propertyFileName, out var keyPrefix))
                {
                    IDictionary<string, string> props = null;
                    // TODO: context.Except
                    var propertyResource = nextInChain.Extract(context, propertyFileName);
                    switch (propertyResource)
                    {
                        case JavaPropertiesResourceInfo jp:
                            props = jp.Data;
                            break;
                        default:
                            var stream = propertyResource?.Open();
                            if (stream != null)
                            {
                                try
                                {
                                    var jp = new JavaProperties();
                                    jp.Load(stream);
                                    props = jp;
                                }
                                catch { }
                                finally { stream.Dispose(); }
                            }
                            break;
                    }

                    string result = null;
                    if (props?.TryGetValue($"{keyPrefix}{name}", out result) ?? false)
                    {
                        return new TextResourceInfo(name, context.Culture, result);
                    }
                }
            }
            return null;
        }
    }
}