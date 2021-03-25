using System;

namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that is used to mark the target method to be invoked when all application modules
    /// have been initialized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleReadyAttribute : ModuleCallbackAttribute { }
}