#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;

namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyContainer : IDependencyContext
    {
        object IDependencyContext.Resolve(Type type, string name) => Resolve(type, name);
    }
}
#endif