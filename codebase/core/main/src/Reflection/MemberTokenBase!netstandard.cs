using System;


namespace Axle.Reflection
{
    [Serializable]
    partial class MemberTokenBase<T>
    {
        protected MemberTokenBase(Type declaringType, string name)
        {
            this.name = name;
            this.declaringType = declaringType;
            this.typeHandle = declaringType.TypeHandle;
        }
    }

    [Serializable]
    partial class MemberTokenBase<T, THandle> { }
}