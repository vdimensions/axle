using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif
    public partial class PropertyToken : MemberTokenBase<PropertyInfo>, IProperty, IEquatable<PropertyToken>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyInfo property;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type memberType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType declaration;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyGetAccessor getAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertySetAccessor setAccessor;

        public bool Equals(PropertyToken other)
        {
            return base.Equals(other);/*
                && DeclaringType == other.DeclaringType
                && MemberType == other.MemberType
                && ((Name == null && other.Name == null) || 
                    (Name != null && Name.Equals(other.Name, StringComparison.Ordinal)))
                && object.Equals(_getAccessor, other._getAccessor)
                && object.Equals(_setAccessor, other._setAccessor);
                */
        }

        IAccessor IAccessible.FindAccessor(AccessorType accessorType)
        {
            switch (accessorType)
            {
                case AccessorType.Get:  return getAccessor;
                case AccessorType.Set:  return setAccessor;
                default:                return null;
            }
        }

        public override PropertyInfo ReflectedMember { get { return property; } }
        public override Type MemberType { get { return memberType; } }
        public override DeclarationType Declaration { get { return declaration; } }
        public override AccessModifier AccessModifier { get { return accessModifier; } }
        IEnumerable<IAccessor> IAccessible.Accessors { get { return new IAccessor[]{GetAccessor, SetAccessor}; } }
        public IGetAccessor GetAccessor { get { return getAccessor; } }
        public ISetAccessor SetAccessor { get { return setAccessor; } }
    }
}