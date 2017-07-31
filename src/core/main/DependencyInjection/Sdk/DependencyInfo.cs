using System;
using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    public class DependencyInfo
    {
        public DependencyInfo(Type type, string name)
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