using System;

using Axle.References;


namespace Axle.Core.Infrastructure.DependencyInjection
{
    public interface IDependencyReference : IReference
    {
        Type Type { get; }
        string Name { get; }
    }
}