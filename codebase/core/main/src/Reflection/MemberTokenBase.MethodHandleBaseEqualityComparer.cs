namespace Axle.Reflection
{
    public abstract partial class MemberTokenBase<T>
    {
        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        [System.Serializable]
        #endif
        private class MethodHandleBaseEqualityComparer<TT> : AbstractEqualityComparer<TT> where TT: MemberTokenBase<T>
        {
            internal MethodHandleBaseEqualityComparer() { }

            protected override int DoGetHashCode(TT obj) { return obj.ReflectedMember.GetHashCode(); }

            protected override bool DoEquals(TT x, TT y) { return x.ReflectedMember.Equals(y.ReflectedMember); }
        }

        #if !NETSTANDARD || NETSTANDARD1_5_OR_NEWER
        private static AbstractEqualityComparer<MemberTokenBase<T>> EqualityComparer
        {
            get { return Axle.References.Singleton<MethodHandleBaseEqualityComparer<MemberTokenBase<T>>>.Instance; }
        }
        #else
        private static AbstractEqualityComparer<MemberTokenBase<T>> comparer = new MethodHandleBaseEqualityComparer<MemberTokenBase<T>>();
        private static AbstractEqualityComparer<MemberTokenBase<T>> EqualityComparer => comparer;
        #endif
    }

    public abstract partial class MemberTokenBase<T, THandle>
    {
        #if !NETSTANDARD
        [System.Serializable]
        #endif
        private sealed class MethodHandleBaseEqualityComparer : AbstractEqualityComparer<MemberTokenBase<T, THandle>>
        {
            internal MethodHandleBaseEqualityComparer() { }

            protected override int DoGetHashCode(MemberTokenBase<T, THandle> obj)
            {
                return obj.Handle.GetHashCode();
            }

            protected override bool DoEquals(MemberTokenBase<T, THandle> x, MemberTokenBase<T, THandle> y)
            {
                return x.Handle.Equals(y.Handle);
            }
        }

        #if !NETSTANDARD || NETSTANDARD1_5_OR_NEWER
        private static AbstractEqualityComparer<MemberTokenBase<T, THandle>> EqualityComparer
        {
            get { return Axle.References.Singleton<MethodHandleBaseEqualityComparer>.Instance; }
        }
        #else
        private static readonly AbstractEqualityComparer<MemberTokenBase<T, THandle>> comparer = new MethodHandleBaseEqualityComparer();
        private static AbstractEqualityComparer<MemberTokenBase<T, THandle>> EqualityComparer => comparer;
        #endif
    }
}