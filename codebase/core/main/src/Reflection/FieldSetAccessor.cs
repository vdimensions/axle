namespace Axle.Reflection
{
    #if NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class FieldSetAccessor : FieldAccessor
    {
        public FieldSetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Set) { }
    }
}