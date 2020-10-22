using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

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
        /// Initializes a new instance of the <see cref="ProvidesForAttribute"/> class with
        /// a <see cref="string">string</see> representation of the module type the target module will be come a
        /// dependency to.
        /// </summary>
        /// <param name="module">
        /// A <see cref="string">string</see> representation of the module type the target module will be come a
        /// dependency to.
        /// </param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ProvidesForAttribute(string module)
        {
            Module = module.VerifyArgument(nameof(module)).IsNotNullOrEmpty();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvidesForAttribute"/> class.
        /// </summary>
        /// <param name="moduleType">
        /// The type of the module that the target module will become a dependency to.
        /// </param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ProvidesForAttribute(Type moduleType)
        {
            Module = UtilizesAttribute.TypeToString(moduleType.VerifyArgument(nameof(moduleType)).IsNotNull());
        }

        internal bool Accepts(Type type) 
            => UtilizesAttribute
                .TypeToString(type.VerifyArgument(nameof(type)).IsNotNull())
                .Equals(Module, StringComparison.Ordinal);

        /// <summary>
        /// Gets the string representation of the type of module that this module will become a dependency of.
        /// </summary>
        public string Module { get; }
    }
}