using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    [Serializable]
    partial class FieldToken : MemberTokenBase<FieldInfo, RuntimeFieldHandle>
    {
        public FieldToken(FieldInfo info) : base(info, info.FieldHandle, info.DeclaringType, info.Name)
        {
            accessModifier = GetAccessModifier(info);
            declaration = info.GetDeclarationType();
            accessors = new FieldAccessor[] { new FieldGetAccessor(this), new FieldSetAccessor(this) };
        }

        protected override FieldInfo GetMember(RuntimeFieldHandle handle, RuntimeTypeHandle typeHandle, bool isGeneric)
        {
            return isGeneric ? FieldInfo.GetFieldFromHandle(handle, typeHandle) : FieldInfo.GetFieldFromHandle(handle);
        }
    }
}