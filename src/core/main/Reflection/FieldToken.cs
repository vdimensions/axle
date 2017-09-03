using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public partial class FieldToken : IEquatable<FieldToken>, IField
    {
        public static AccessModifier GetAccessModifier(FieldInfo fieldInfo)
        {
            return GetAccessModifier(fieldInfo.IsPublic, fieldInfo.IsAssembly, fieldInfo.IsFamily, fieldInfo.IsPrivate);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly FieldAccessor[] _accessors;

        public override bool Equals(object obj) { return obj is FieldToken && Equals((FieldToken) obj); }
        public bool Equals(FieldToken other) { return base.Equals(other); }

        IAccessor IAccessible.FindAccessor(AccessorType accessorType)
        {
            switch (accessorType)
            {
                case AccessorType.Get:  return GetAccessor;
                case AccessorType.Set:  return SetAccessor;
                default:                return null;
            }
        }

        public override int GetHashCode() { return unchecked(base.GetHashCode()); }
        
        public IGetAccessor GetAccessor => _accessors[0];
        public ISetAccessor SetAccessor => _accessors[1];
        public bool IsReadOnly => ReflectedMember.IsInitOnly;
        IEnumerable<IAccessor> IAccessible.Accessors => _accessors;
        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
        public override Type MemberType => ReflectedMember.FieldType;
    }
}