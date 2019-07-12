using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Axle.References;
using Axle.Resources;
using Axle.Resources.Extraction;


namespace Axle.Data.Resources.Extraction
{
    internal sealed class SqlScriptSourceExtractor : AbstractResourceExtractor
    {
        private const int BufferSize = 8096;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<string> _dialects;

        internal SqlScriptSourceExtractor(IEnumerable<string> dialects)
        {
            _dialects = dialects;
        }

        protected override Nullsafe<ResourceInfo> DoExtract(ResourceContext context, string name)
        {
            if (name.EndsWith(SqlScriptSourceInfo.FileExtension, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            var strComparer = StringComparer.Ordinal;
            IDictionary<string, string> scripts = new Dictionary<string, string>(strComparer);
            foreach (var dialect in _dialects.Distinct(strComparer).Select(d => $"{d}/").Union(new[]{string.Empty}))
            {
                var queryResource = context.ExtractionChain.Extract($"{dialect}{name}{SqlScriptSourceInfo.FileExtension}");
                if (!queryResource.HasValue)
                {
                    continue;
                }
                using (var stream = queryResource.Value.Open())
                using (var reader = new StreamReader(stream, Encoding.UTF8, true, BufferSize))
                {
                    var scriptStr = reader.ReadToEnd();
                    scripts.Add(dialect, scriptStr);
                }
            }
            return scripts.Count != 0 
                ? new SqlScriptSourceInfo(new SqlScriptSource(name, scripts), context.Culture) 
                : null;
        }
    }
}