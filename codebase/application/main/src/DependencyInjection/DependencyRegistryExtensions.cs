using System;

using Axle.Verification;


namespace Axle.DependencyInjection
{
    public static class DependencyRegistryExtensions
    {
        public static IDependencyRegistry RegisterType(this IDependencyRegistry dependencyContainer, Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(dependencyContainer, nameof(dependencyContainer)));
            return dependencyContainer.RegisterType(type, string.Empty);
        }
        public static IDependencyRegistry RegisterType<T>(this IDependencyRegistry dependencyContainer, string name, params string[] aliases)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(dependencyContainer, nameof(dependencyContainer)));
            return dependencyContainer.RegisterType(typeof(T), name, aliases);
        }
        public static IDependencyRegistry RegisterType<T>(this IDependencyRegistry dependencyContainer)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(dependencyContainer, nameof(dependencyContainer)));
            return dependencyContainer.RegisterType(typeof(T), string.Empty);
        }
    }
}
