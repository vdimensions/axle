using System;


namespace Axle.Modularity
{
    /// <summary>
    /// A module lifecycle attribute that is used to identify a module's entry point method.
    /// Module entry point methods are invoked when the entire application is started and all module dependencies
    /// are resolved. If an application includes multiple modules with entry points, they will be invoked sequentially
    /// in the dependency resolution order for those modules.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleEntryPointAttribute : ModuleCallbackAttribute { }
}