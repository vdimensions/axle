using System;

using Axle.Verification;


namespace Axle.Core.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency injection attribute - allows a dependency provider to inject the appropriate values
    /// to the properties of an object or to pass them to any of its constructors marked with this attribute.
    /// </summary>
    /// <remarks>
    /// The decorated properties, that are not initialized from an injection constructor, must have a setter.
    /// </remarks>
    [AttributeUsage(Targets, Inherited = true, AllowMultiple = false)]
    public sealed class InjectAttribute : Attribute
    {
        private const AttributeTargets Targets = AttributeTargets.Constructor|AttributeTargets.Parameter|AttributeTargets.Property;

        public InjectAttribute(string name)
        {
            Name = name.VerifyArgument(nameof(name)).IsNotNull();
        }
        public InjectAttribute() : this(string.Empty) { }

        /// <summary>
        /// Gets the binding name that the injected object should be registered with, or empty
        /// string when binding names are not applicable.
        /// </summary>
        public string Name { get; }
    }
}