namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class FieldSetAccessor : FieldAccessor
    {
        public FieldSetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Set) { }
    }
}