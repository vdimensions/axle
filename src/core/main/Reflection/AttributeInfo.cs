using System;
using System.Diagnostics;


namespace Axle.Reflection
{
    #if !netstandard    
    [Serializable]
    #endif
    internal sealed class AttributeInfo : IAttributeInfo
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AttributeUsageAttribute attributeUsage;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Attribute attribute;

        public AttributeInfo(Attribute attribute, AttributeUsageAttribute attributeUsage)
        {
            this.attribute = attribute;
            this.attributeUsage = attributeUsage;
        }

        internal AttributeUsageAttribute AttributeUsage { get { return attributeUsage; } }
        Attribute IAttributeInfo.Attribute { get { return attribute; } }
        AttributeTargets IAttributeInfo.AttributeTargets { get { return AttributeUsage.ValidOn; } }
        bool IAttributeInfo.AllowMultiple { get { return AttributeUsage.AllowMultiple; } }
        bool IAttributeInfo.Inherited { get { return AttributeUsage.Inherited; } }
    }
}