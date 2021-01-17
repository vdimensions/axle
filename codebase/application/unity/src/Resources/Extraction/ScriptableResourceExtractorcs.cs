using System;
using Axle.Resources;
using Axle.Resources.Extraction;
using UnityEngine;
#if !UNITY_WEBGL
using System.Threading.Tasks;
#endif

namespace Axle.Application.Unity.Resources.Extraction
{
    [Serializable]
    public abstract class ScriptableResourceExtractor : ScriptableObject, IResourceExtractor
    {
        private sealed class InnerExtractor : AbstractResourceExtractor
        {
            private readonly ScriptableResourceExtractor _sre;

            public InnerExtractor(ScriptableResourceExtractor sre)
            {
                _sre = sre;
            }

            protected override ResourceInfo DoExtract(IResourceContext context, string name) => _sre.DoExtract(context, name);
        }
        protected virtual ResourceInfo DoExtract(IResourceContext context, string name) => context.Extract(name);

        public ResourceInfo Extract(IResourceContext context, string name)
        {
            return new InnerExtractor(this).Extract(context, name);
        }
        #if !UNITY_WEBGL
        #if NETSTANDARD || NET45_OR_NEWER
        public async Task<Nullsafe<ResourceInfo>> ExtractAsync(ResourceContext context, string name) 
            => await new InnerExtractor(this).ExtractAsync(context, name);
        #else
        public Task<ResourceInfo> ExtractAsync(IResourceContext context, string name) 
            => new InnerExtractor(this).ExtractAsync(context, name);
        #endif
        #endif
    }
}
