namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface IConstructor : IInvokable, IMember
    {
        object Invoke(params object[] args);
    }
}