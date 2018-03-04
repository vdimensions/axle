using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Axle.Resources;
using Axle.Resources.Extraction;


namespace Axle.Data.Resources.Extraction
{
    internal sealed class QueryResourceExtractor : IResourceExtractor
    {
        private const int BufferSize = 8096;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<string> _dialects;

        internal QueryResourceExtractor(IEnumerable<string> dialects)
        {
            _dialects = dialects;
        }

        ResourceInfo IResourceExtractor.Extract(ResourceContext context, string name)
        {
            if (name.EndsWith(QueryResourceInfo.FileExtension, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            var strComparer = StringComparer.Ordinal;
            IDictionary<string, string> scripts = new Dictionary<string, string>(strComparer);
            var dialects = _dialects.Distinct(strComparer);
            foreach (var dialect in dialects.Select(d => $"{d}/").Union(new[]{string.Empty}))
            {
                var queryResource = context.ExtractionChain.Extract($"{dialect}{name}{QueryResourceInfo.FileExtension}");
                using (var stream = queryResource?.Open())
                using (var reader = stream != null ? new StreamReader(stream, Encoding.UTF8, true, BufferSize) : null)
                {
                    if (reader == null)
                    {
                        continue;
                    }
                    var scriptStr = reader.ReadToEnd();
                    scripts.Add(dialect, scriptStr);
                }
            }
            return scripts.Count != 0 
                ? new QueryResourceInfo(new Query(name, scripts), context.Culture) 
                : null;
        }
    }
}