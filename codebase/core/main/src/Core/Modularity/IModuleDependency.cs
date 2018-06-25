namespace Axle.Core.Modularity
{
    /// <summary>
    /// An interface representing a dependency to a module.
    /// </summary>
    /// <seealso cref="IApplicationModule"/>
    public interface IModuleDependency
    {
        /// <summary>
        /// Gets the name of the <see cref="IApplicationModule">application module</see> that should become a dependency.
        /// </summary>
        string DependencyName { get; }

        /// <summary>
        /// Gets a <see cref="bool">boolean</see> value indicating whether the <b>dependent</b> <see cref="IApplicationModule">module</see> 
        /// should notify its <b>dependency</b> after it was initialized.
        /// </summary>
        bool NotifyOnInit { get; }

        ///// <summary>
        ///// Gets a <see cref="bool">boolean</see> value indicating whether the <b>dependent</b> <see cref="IApplicationModule">module</see> 
        ///// should notify its <b>dependency</b> after it was loaded.
        ///// </summary>
        //bool NotifyOnLoad { get; }

        /// <summary>
        /// Gets a <see cref="bool">boolean</see> value indicating whether the <b>dependency</b> module should expose the objects 
        /// from its <see cref="IApplicationModule.Container">dependency container</see> to the <b>dependent</b> module.
        /// </summary>
        /// <remarks>
        /// A <see cref="IApplicationModule">module</see> can have multiple dependencies, but <b>only a single dependency module</b> 
        /// is allowed to share its dependencies with the <b>dependent</b>  module.
        /// </remarks>
        bool ShareDependencies { get; }
    }
}