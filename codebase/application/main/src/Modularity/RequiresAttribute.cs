using System;


namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that is used to establish a specified module as a dependency on the target module.
    /// </summary>
    /// <seealso cref="UtilizesAttribute"/>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class RequiresAttribute : Attribute, IModuleReferenceAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAttribute"/> class.
        /// </summary>
        /// <param name="moduleType">
        /// The type of the module that will become a dependency for the target module.
        /// </param>
        public RequiresAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }

        bool IModuleReferenceAttribute.Mandatory => true;
        /// <summary>
        /// Gets the type of the module that is set as a dependency on the target module.
        /// </summary>
        public Type ModuleType { get; }
    }
}