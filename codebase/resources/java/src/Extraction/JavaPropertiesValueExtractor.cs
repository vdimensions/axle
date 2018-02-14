using System;
using System.Collections.Generic;

using Axle.Resources.Extraction;
using Axle.Extensions.String;

using Kajabity.Tools.Java;


namespace Axle.Resources.Java.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementation that can access the values defined in a Java properties file.
    /// </summary>
    public sealed class JavaPropertiesValueExtractor : ResourceExtractorChain
    {
        /// <summary>
        /// Creates a new instance of the <see cref="JavaPropertiesValueExtractor"/> class.
        /// </summary>
        public JavaPropertiesValueExtractor() : base() { }

        private bool GetPropertiesFileData(Uri location, out string propertyFileName, out string keyPrefix)
        {
            propertyFileName = keyPrefix = null;
            const string ext = JavaPropertiesResourceInfo.FileExtension;
            var locStr = location.ToString();
            keyPrefix = locStr.TakeAfterFirst(ext);
            propertyFileName = locStr.TakeBeforeFirst(keyPrefix);

            return !string.IsNullOrEmpty(propertyFileName) && propertyFileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        /// <summary>
        /// Attempts to locate a string value with the given <paramref name="name"/> that is defined into a Java properties file. 
        /// </summary>
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
                            using (var stream = propertyResource?.Open())
                            {
                                if (stream != null)
                                {
                                    var jp = new JavaProperties();
                                    jp.Load(stream);
                                    props = jp;
                                }
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