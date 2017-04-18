using System;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface IAttributeInfo
    {
        Attribute Attribute { get; }
        AttributeTargets AttributeTargets { get; }
        bool AllowMultiple { get; }
        bool Inherited { get; }
    }
}