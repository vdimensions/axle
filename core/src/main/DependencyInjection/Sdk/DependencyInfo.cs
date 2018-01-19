using System;

using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    /// <summary>
    /// A class that describes a dependency which needs to be satisfied by a 
    /// <see cref="IContainer">dependency container</see> during object construction.
    /// </summary>
    public class DependencyInfo
    {
        public DependencyInfo(Type type, string dependencyName, string memberName)
        {
            Type = type.VerifyArgument(nameof(type)).IsNotNull();
            DependencyName = dependencyName.VerifyArgument(nameof(dependencyName)).IsNotNull();
            MemberName = memberName.VerifyArgument(nameof(memberName)).IsNotNull();
        }

        /// <summary>
        /// Gets the type of the dependency
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the name of the dependency, or <c>String.Empty</c> if the dependency is not explicitly named.
        /// </summary>
        public string DependencyName { get; }

        /// <summary>
        /// The name of the reflected member that is represented by the current <see cref="DependencyInfo">instance</see>.
        /// </summary>
        public string MemberName { get; }
    }
}