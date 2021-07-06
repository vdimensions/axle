using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that allows the annotated module to become a dependency of the module specified by the attribute.
    /// The annotated module itself will receive as dependencies all the required dependencies of the module that was
    /// specified.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class ProvidesForAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvidesForAttribute"/> class.
        /// </summary>
        /// <param name="moduleType">
        /// The type of the module that the target module will become a dependency to.
        /// </param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ProvidesForAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }

        internal bool Accepts(Type type)
        {
            return ModuleType == type;
        }

        /// <summary>
        /// Gets the string representation of the type of module that this module will become a dependency of.
        /// </summary>
        public Type ModuleType { get; }
    }
}