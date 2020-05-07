using System;


namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that is used to make a module's method respond to lifecycle events of modules that depend on the
    /// current module. The annotated method will be called when another module depending on the current module
    /// successfully terminates, and that module will be passed in as parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleDependencyTerminatedAttribute : ModuleDependencyCallbackAttribute { }
}