using System;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif
    internal sealed class FieldTokenGetAccessor : FieldTokenAccessor
    {
        public FieldTokenGetAccessor(FieldToken fieldToken) : base(fieldToken, AccessorType.Get) { }
    }
}