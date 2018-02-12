using System;


namespace Axle.Reflection
{
    public abstract partial class MemberTokenBase<T>
    {
        protected struct AttributeInfo : IAttributeInfo
        {
            public Attribute Attribute { get; internal set; }
            public AttributeTargets AttributeTargets { get; internal set; }
            public bool AllowMultiple { get; internal set; }
            public bool Inherited { get; internal set; }
        }
    }
}