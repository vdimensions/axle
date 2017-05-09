using System;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif
    internal abstract class FieldAccessor : IGetAccessor, ISetAccessor, IReflected<FieldInfo>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly FieldToken fieldToken;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessorType accessorType;

        protected FieldAccessor(FieldToken fieldToken, AccessorType accessorType)
        {
            this.fieldToken = fieldToken;
            this.accessorType = accessorType;
        }

        object IGetAccessor.GetValue(object target) { return fieldToken.ReflectedMember.GetValue(target); }

        void ISetAccessor.SetValue(object target, object value) { fieldToken.ReflectedMember.SetValue(target, value); }

        DeclarationType IAccessor.Declaration { get { return fieldToken.Declaration; } }
        AccessModifier IAccessor.AccessModifier { get { return fieldToken.AccessModifier; } }
        IMember IAccessor.Member { get { return fieldToken; } }
        public AccessorType AccessorType { get { return accessorType; } }
        MemberInfo IReflected.ReflectedMember { get { return fieldToken.ReflectedMember; } }
        FieldInfo IReflected<FieldInfo>.ReflectedMember { get { return fieldToken.ReflectedMember; } }
    }
}