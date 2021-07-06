namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An <see cref="IResourceExtractor"/> implementation that does not do any resource extraction work.
    /// If the provided <see cref="IResourceContext"/> has a continuation chain, this implementation will invoke the
    /// chain immediately.
    /// </summary>
    public sealed class NoopResourceExtractor : AbstractResourceExtractor { }
}