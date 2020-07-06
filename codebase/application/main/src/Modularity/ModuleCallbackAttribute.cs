using System;


namespace Axle.Modularity
{
    /// <summary>
    /// An abstract attribute class serving as a base for module callback attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public abstract class ModuleCallbackAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleCallbackAttribute"/> class with the specified
        /// <paramref name="allowParallelInvoke"/> value.
        /// <param name="allowParallelInvoke">
        /// A <see cref="bool">boolean</see> flag indicating whether the target module callback can be invoked
        /// from a different thread than the thread instantiating the module.
        /// </param>
        /// </summary>
        protected ModuleCallbackAttribute(bool allowParallelInvoke) { AllowParallelInvoke = allowParallelInvoke; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleCallbackAttribute"/> class.
        /// </summary>
        protected ModuleCallbackAttribute() : this(false) { }

        /// <summary>
        /// A <see cref="bool">boolean</see> flag indicating whether the target module callback can be invoked
        /// from a different thread than the thread instantiating the module.
        /// </summary>
        public bool AllowParallelInvoke { get; set; }
    }
}