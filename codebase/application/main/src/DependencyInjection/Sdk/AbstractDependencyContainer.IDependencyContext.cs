#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;

namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyContainer : IDependencyContext
    {
        object IDependencyContext.Resolve(Type type, string name) => Resolve(type, name);
    }
}
#endif