using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
    [Serializable]
    #endif
	public class PropertyToken : MemberTokenBase<PropertyInfo>, IProperty, IEquatable<PropertyToken>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyInfo _property;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _memberType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyGetAccessor _getAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertySetAccessor _setAccessor;

        public PropertyToken(PropertyInfo propertyInfo) : base(propertyInfo, propertyInfo.DeclaringType, propertyInfo.Name)
        {
            _property = propertyInfo;
            #if NETSTANDARD
            var gm = propertyInfo.GetMethod;
            var sm = propertyInfo.SetMethod;
            #else
            var gm = propertyInfo.GetGetMethod(true);
            var sm = propertyInfo.GetSetMethod(true);
            #endif
            _getAccessor = gm != null ? new PropertyGetAccessor(this, new MethodToken(gm)) : null;
            _setAccessor = sm != null ? new PropertySetAccessor(this, new MethodToken(sm)) : null;
            _memberType = propertyInfo.PropertyType;

            _declaration = ReflectionExtensions.GetDeclarationTypeUnchecked(gm, sm);

            var isPublic = (gm == null || gm.IsPublic) && (sm == null || sm.IsPublic);
            var isAssembly = (gm == null || gm.IsAssembly) && (sm == null || sm.IsAssembly) && !isPublic;
            var isFamily = (gm == null || gm.IsFamily) && (sm == null || sm.IsFamily) && !isPublic;
            var isPrivate = (gm == null || gm.IsPrivate) && (sm == null || sm.IsPrivate) &&
                            !(isPublic || isFamily || isAssembly);
            _accessModifier = GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
        }

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
                case AccessorType.Get:  return _getAccessor;
                case AccessorType.Set:  return _setAccessor;
                default:                return null;
            }
        }

        public override PropertyInfo ReflectedMember => _property;
        public override Type MemberType => _memberType;
        public override DeclarationType Declaration => _declaration;
        public override AccessModifier AccessModifier => _accessModifier;
        IEnumerable<IAccessor> IAccessible.Accessors => new IAccessor[]{GetAccessor, SetAccessor};
        public IGetAccessor GetAccessor => _getAccessor;
        public ISetAccessor SetAccessor => _setAccessor;
    }
}