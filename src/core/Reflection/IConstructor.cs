namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface IConstructor : IInvokable, IMember, IAttributeTarget
    {
        object Invoke(params object[] args);
    }
}