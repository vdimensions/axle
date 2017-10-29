using System.Reflection;


namespace Axle.Reflection
{
    partial class FieldToken : MemberTokenBase<FieldInfo>
    {
        public FieldToken(FieldInfo info) : base(info, info.DeclaringType, info.Name)
        {
            _accessModifier = GetAccessModifier(info);
            _declaration = info.GetDeclarationType();
            _accessors = new FieldAccessor[] { new FieldGetAccessor(this), new FieldSetAccessor(this) };
        }
    }
}