#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class FieldGetAccessor : FieldAccessor
    {
        public FieldGetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Get) { }
    }
}
#endif