using System;
using System.Diagnostics;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    internal sealed class AttributeInfo : IAttributeInfo
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AttributeUsageAttribute _attributeUsage;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Attribute _attribute;

        public AttributeInfo(Attribute attribute, AttributeUsageAttribute attributeUsage)
        {
            _attribute = attribute;
            _attributeUsage = attributeUsage;
        }

        internal AttributeUsageAttribute AttributeUsage => _attributeUsage;
        Attribute IAttributeInfo.Attribute => _attribute;
        AttributeTargets IAttributeInfo.AttributeTargets => AttributeUsage.ValidOn;
        bool IAttributeInfo.AllowMultiple => AttributeUsage.AllowMultiple;
        bool IAttributeInfo.Inherited => AttributeUsage.Inherited;
    }
}