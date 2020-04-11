using System;

namespace Axle.Modularity
{
    internal interface IModuleReferenceAttribute
    {
        Type ModuleType { get; }
        bool Mandatory { get; }
    }
}