using System.Collections.Generic;

namespace Axle.DependencyInjection.Sdk
{
    public abstract class InjectionDescriptor<T>
    {
        public IEnumerable<T> Dependencies { get; }
    }
}