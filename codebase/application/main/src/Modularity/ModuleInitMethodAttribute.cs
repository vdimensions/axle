using System;


namespace Axle.Application.Modularity
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleInitMethodAttribute : ModuleCallbackAttribute { }
}