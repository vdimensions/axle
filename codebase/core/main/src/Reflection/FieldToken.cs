using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    public class FieldToken : MemberTokenBase<FieldInfo, RuntimeFieldHandle>,
    #else
    public class FieldToken : MemberTokenBase<FieldInfo>,
    #endif
        IEquatable<FieldToken>, IField
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly FieldAccessor[] _accessors;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public FieldToken(FieldInfo info) : base(info, info.FieldHandle, info.DeclaringType, info.Name)
        #else
        public FieldToken(FieldInfo info) : base(info, info.DeclaringType, info.Name)
        #endif
        {
            _accessModifier = GetAccessModifier(info);
            _declaration = ReflectionExtensions.GetDeclarationType(info);
            _accessors = new FieldAccessor[] { new FieldGetAccessor(this), new FieldSetAccessor(this) };
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected override FieldInfo GetMember(RuntimeFieldHandle handle, RuntimeTypeHandle typeHandle, bool isGeneric)
        {
            return isGeneric ? FieldInfo.GetFieldFromHandle(handle, typeHandle) : FieldInfo.GetFieldFromHandle(handle);
        }
        #endif

        public static AccessModifier GetAccessModifier(FieldInfo fieldInfo) =>
            AccessModifierExtensions.GetAccessModifier(fieldInfo.IsPublic, fieldInfo.IsAssembly, fieldInfo.IsFamily, fieldInfo.IsPrivate);

        public override bool Equals(object obj) => obj is FieldToken f && Equals(f);
        public bool Equals(FieldToken other) => base.Equals(other);

        IAccessor IAccessible.FindAccessor(AccessorType accessorType)
        {
            switch (accessorType)
            {
                case AccessorType.Get:
                    return GetAccessor;
                case AccessorType.Set:
                    return SetAccessor;
                default:
                    return null;
            }
        }

        public override int GetHashCode() => base.GetHashCode();

        public IGetAccessor GetAccessor => _accessors[0];
        public ISetAccessor SetAccessor => _accessors[1];
        public bool IsReadOnly => ReflectedMember.IsInitOnly;
        IEnumerable<IAccessor> IAccessible.Accessors => _accessors;
        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
        public override Type MemberType => ReflectedMember.FieldType;
    }
}