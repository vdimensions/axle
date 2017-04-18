using System.Reflection;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface IReflected
    {
        MemberInfo ReflectedMember { get; }
    }
    //[Maturity(CodeMaturity.Stable)]
    public interface IReflected<T> : IReflected where T: MemberInfo
    {
        new T ReflectedMember { get; }
    }
}