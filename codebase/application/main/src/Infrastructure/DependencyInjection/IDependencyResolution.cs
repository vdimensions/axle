using System;


namespace Axle.Core.Infrastructure.DependencyInjection
{
    public interface IDependencyResolution
    {
        bool IsSingleton { get; }
        bool Succeeded { get; }
        Type Type { get; }
        object Value { get; }
    }
}