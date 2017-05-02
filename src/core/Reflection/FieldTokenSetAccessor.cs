using System;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif
    internal sealed class FieldTokenSetAccessor : FieldTokenAccessor
    {
        public FieldTokenSetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Set) { }
    }
}