using System;


namespace Axle.Modularity
{
    /// <summary>
    /// A module lifecycle attribute that is used to indicate the module initialization method.
    /// A module can optionally provide such method by annotating it with this attribute. Usually this is the phase
    /// where the module will export dependencies so that these are available for injection to modules following the
    /// initialization of the current module.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleInitAttribute : ModuleCallbackAttribute { }
}