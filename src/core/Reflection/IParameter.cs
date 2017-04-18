using System;


namespace Axle.Reflection
{
    public interface IParameter : IAttributeTarget
    {
        Type Type { get; }
        string Name { get; }
        bool IsOptional { get; }
        object DefaultValue { get; }
    }
}