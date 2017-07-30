using System;

using Axle.References;
using Axle.Verification;


namespace Axle.DependencyInjection.Descriptors
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

    public interface IDependencyDescriptor
    {
    }

    internal sealed class DependencyDescriptor<T> : IDependencyDescriptor
    {
        public static DependencyDescriptor<T> Instance => Singleton<DependencyDescriptor<T>>.Instance;

        private DependencyDescriptor() { }
    }

    internal static class DependencyDescriptor
    {
        public static IDependencyDescriptor Get<T>() { return DependencyDescriptor<T>.Instance; }
    }
}