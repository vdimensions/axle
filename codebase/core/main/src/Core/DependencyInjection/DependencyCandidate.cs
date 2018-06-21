using System;

using Axle.Verification;


namespace Axle.Core.DependencyInjection
{
    /// <summary>
    /// A class that describes a candidate dependency which can satisfy a dependency when a 
    /// <see cref="IContainer">dependency container</see> is instantiating an object.
    /// </summary>
    public sealed class DependencyCandidate
    {
        public DependencyCandidate(Type type, string name)
        {
            Type = type.VerifyArgument(nameof(type)).IsNotNull();
            Name = name.VerifyArgument(nameof(name)).IsNotNull();
        }

        /// <summary>
        /// Gets the type of the dependency
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the name of the dependency, or <c>String.Empty</c> if the dependency is not explicitly named.
        /// </summary>
        public string Name { get; }
    }
}