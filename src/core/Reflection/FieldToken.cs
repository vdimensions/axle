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
        private readonly AccessModifier accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType declaration;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly FieldTokenAccessor[] accessors;

        public override bool Equals(object obj) { return obj is FieldToken && base.Equals(obj); }
        bool IEquatable<FieldToken>.Equals(FieldToken other) { return base.Equals(other); }

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
        
        public IGetAccessor GetAccessor { get { return accessors[0]; } }
        public ISetAccessor SetAccessor { get { return accessors[1]; } }
        IEnumerable<IAccessor> IAccessible.Accessors { get { return accessors; } }
        public override AccessModifier AccessModifier { get { return accessModifier; } }
        public override DeclarationType Declaration { get { return declaration; } }
        public override Type MemberType { get { return ReflectedMember.FieldType; } }
    }
}