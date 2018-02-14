namespace Axle.Resources.Extraction
{
    /// <summary>
    /// A resource context split strategy enumeration. 
    /// Provides different means of iterating over the source locations and cultures
    /// of a <see cref="ResourceContext"/> instance in order to materialize complex 
    /// resource objects.
    /// </summary>
    public enum ResourceContextSplitStrategy
    {
        ByLocation = 0,
        ByCulture,
        ByLocationThenCulture,
        ByCultureThenLocation
    }
}