using System.Reflection;


namespace Axle.Reflection
{
    partial class MethodToken
    {
        public MethodToken(MethodInfo info) : base(info)
        {
            memberType = (ReflectedMember = info).ReturnType;
        }

        public override MethodInfo ReflectedMember { get; }
    }
}