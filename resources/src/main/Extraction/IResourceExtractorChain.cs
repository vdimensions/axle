using System.Globalization;


namespace Axle.Resources.Extraction
{
    public interface IResourceExtractorChain : IResourceExtractor
    {
        ResourceInfo Extract(string name, CultureInfo culture, IResourceExtractor nextInChain);
    }
}