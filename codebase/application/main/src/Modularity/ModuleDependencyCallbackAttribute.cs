namespace Axle.Modularity
{
    /// <summary>
    /// An abstract class serving as a base for module dependency callback attributes.
    /// </summary>
    /// <seealso cref="ModuleDependencyInitializedAttribute"/>
    /// <seealso cref="ModuleDependencyTerminatedAttribute"/>
    public abstract class ModuleDependencyCallbackAttribute : ModuleCallbackAttribute
    {
        public int Priority { get; set; } = 0;
    }
}