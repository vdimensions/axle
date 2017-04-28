using System.Reflection;


namespace Axle.Reflection
{
    partial class FieldToken : MemberTokenBase<FieldInfo>
    {
        public FieldToken(FieldInfo info) : base(info, info.DeclaringType, info.Name)
        {
            accessModifier = GetAccessModifier(info);
            declaration = info.GetDeclarationType();
            accessors = new FieldTokenAccessor[] { new FieldTokenGetAccessor(this), new FieldTokenSetAccessor(this) };
        }
    }
}