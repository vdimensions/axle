using System;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif
    internal sealed class FieldSetAccessor : FieldAccessor
    {
        public FieldSetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Set) { }
    }
}