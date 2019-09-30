using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Axle.References;
using Axle.Resources;
using Axle.Resources.Extraction;

namespace Axle.Data.DataSources.Resources.Extraction
{
    internal sealed class SqlScriptSourceExtractor : AbstractResourceExtractor
    {
        private const int BufferSize = 8192;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<string> _dialects;

        internal SqlScriptSourceExtractor(IEnumerable<string> dialects)
        {
            _dialects = dialects;
        }

        protected override Nullsafe<ResourceInfo> DoExtract(ResourceContext context, string name)
        {
            string[] nameVariants;
            if (!name.EndsWith(SqlScriptSourceInfo.FileExtension, StringComparison.OrdinalIgnoreCase))
            {
                var extensionVariants = new[]
                {
                    SqlScriptSourceInfo.FileExtension,
                    SqlScriptSourceInfo.FileExtension.ToUpperInvariant()
                };
                nameVariants = Enumerable.ToArray(Enumerable.Select(extensionVariants, ext => $"{name}{ext}"));
            }
            else
            {
                nameVariants = new[] {name};
            }
            var strComparer = StringComparer.Ordinal;
            var dialectList = Enumerable.Union(
                Enumerable.Distinct(_dialects, strComparer), 
                new[]{string.Empty});
            IDictionary<string, string> scripts = new Dictionary<string, string>(strComparer);
            foreach (var dialect in dialectList)
            {
                var queryResource = nameVariants
                    .Select(p => new { Path = Path.GetDirectoryName(p), File = Path.GetFileName(p) })
                    .Select(p => Path.Combine(Path.Combine(p.Path, dialect), p.File))
                    .Select(context.ExtractionChain.Extract)
                    .Where(r => r.HasValue)
                    .Select(r => r.Value)
                    .FirstOrDefault();
                if (queryResource == null)
                {
                    continue;
                }
                using (var stream = queryResource.Open())
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