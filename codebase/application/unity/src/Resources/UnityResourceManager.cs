using Axle.Resources;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using UnityEngine;

namespace Axle.Application.Unity.Resources
{
    public sealed class UnityResourceManager : MonoBehaviour
    {
        private ResourceManager _resourceManager;

        public void Init(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public IResourceBundleRegistry Bundles => _resourceManager?.Bundles;
        public IResourceExtractorRegistry Extractors => _resourceManager?.Extractors;
    }
}