#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;
using Axle.Verification;

namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyContainer : IDependencyRegistry
    {
        IDependencyRegistry IDependencyRegistry.RegisterType(Type type, string name, params string[] aliases)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            return RegisterType(type, name, aliases);
        }
    }
}
#endif