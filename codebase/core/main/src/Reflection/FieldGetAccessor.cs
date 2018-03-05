namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class FieldGetAccessor : FieldAccessor
    {
        public FieldGetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Get) { }
    }
}