using System;


namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that is used to mark the target method to be invoked during the termination phase
    /// of a module lifecycle.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleTerminateAttribute : ModuleCallbackAttribute { }
}