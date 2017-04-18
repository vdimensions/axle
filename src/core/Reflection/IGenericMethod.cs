using System;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface IGenericMethod : IMethod
    {
        Type[] GenericArguments { get; }
    }
}