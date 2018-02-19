namespace Axle.Resources.Extraction
{
    public interface IContextExtractionChain
    {
        ResourceInfo Extract(string name);
    }
}