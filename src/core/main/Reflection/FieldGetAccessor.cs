using System;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif
    internal sealed class FieldGetAccessor : FieldAccessor
    {
        public FieldGetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Get) { }
    }
}