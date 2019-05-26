using System;


namespace Axle.Reflection
{
    public interface IGenericMethod : IMethod
    {
        Type[] GenericArguments { get; }
    }
}