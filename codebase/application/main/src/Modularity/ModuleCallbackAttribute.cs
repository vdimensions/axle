using System;


namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public abstract class ModuleCallbackAttribute : Attribute
    {
        protected ModuleCallbackAttribute(bool allowParallelInvoke) { AllowParallelInvoke = allowParallelInvoke; }
        protected ModuleCallbackAttribute() : this(false) { }

        public bool AllowParallelInvoke { get; set; }
    }
}