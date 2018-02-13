using System.Collections.Generic;


namespace Axle.Resources.Extraction
{
    public interface IResourceExtractorRegistry : IEnumerable<IResourceExtractor>
    {
        IResourceExtractorRegistry Register(IResourceExtractor extractor);
    }
}