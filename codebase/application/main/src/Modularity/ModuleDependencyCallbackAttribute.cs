namespace Axle.Modularity
{
    /// <summary>
    /// An abstract class serving as a base for module dependency callback attributes.
    /// </summary>
    /// <seealso cref="ModuleDependencyInitializedAttribute"/>
    /// <seealso cref="ModuleDependencyTerminatedAttribute"/>
    public abstract class ModuleDependencyCallbackAttribute : ModuleCallbackAttribute
    {
        /// <summary>
        /// Gets or sets a number value indicating the execution order for the target callback method.
        /// Callbacks with higher priority values will be executed earlier.
        /// <para>
        /// The default value is <c>0</c>.
        /// </para>
        /// </summary>
        public int Priority { get; set; } = 0;
    }
}