namespace Axle.Reflection
{
    #if NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class FieldGetAccessor : FieldAccessor
    {
        public FieldGetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Get) { }
    }
}