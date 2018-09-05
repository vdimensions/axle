#if NETSTANDARD || NET35_OR_NEWER
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal abstract class FieldAccessor : IGetAccessor, ISetAccessor, IReflected<FieldInfo>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly FieldToken _fieldToken;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessorType _accessorType;

        protected FieldAccessor(FieldToken fieldToken, AccessorType accessorType)
        {
            _fieldToken = fieldToken;
            _accessorType = accessorType;
        }

        object IGetAccessor.GetValue(object target) { return _fieldToken.ReflectedMember.GetValue(target); }

        void ISetAccessor.SetValue(object target, object value) { _fieldToken.ReflectedMember.SetValue(target, value); }

        DeclarationType IAccessor.Declaration => _fieldToken.Declaration;
        AccessModifier IAccessor.AccessModifier => _fieldToken.AccessModifier;
        IMember IAccessor.Member => _fieldToken;
        public AccessorType AccessorType => _accessorType;
        MemberInfo IReflected.ReflectedMember => _fieldToken.ReflectedMember;
        FieldInfo IReflected<FieldInfo>.ReflectedMember => _fieldToken.ReflectedMember;
    }
}
#endif