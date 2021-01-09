#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;

namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyContainer : IDependencyRegistry
    {
        IDependencyRegistry IDependencyRegistry.RegisterType(Type type, string name, params string[] aliases)
        {
            return RegisterType(type, name, aliases);
        }
    }
}
#endif