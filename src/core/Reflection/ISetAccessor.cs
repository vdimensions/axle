namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface ISetAccessor : IAccessor
    {
        void SetValue(object target, object value);
    }
}