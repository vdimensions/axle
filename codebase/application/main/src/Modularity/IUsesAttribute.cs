using System;

namespace Axle.Modularity
{
    internal interface IUsesAttribute
    {
        Type ModuleType { get; }
        bool Required { get; }
    }
}