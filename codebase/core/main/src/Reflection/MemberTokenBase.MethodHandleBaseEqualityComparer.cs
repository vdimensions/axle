namespace Axle.Reflection
{
    abstract partial class MemberTokenBase<T>
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Serializable]
        #endif
        private class MethodHandleBaseEqualityComparer<TT> : AbstractEqualityComparer<TT> where TT: MemberTokenBase<T>
        {
            internal MethodHandleBaseEqualityComparer() { }

            protected override int DoGetHashCode(TT obj) => obj.ReflectedMember.GetHashCode();

            protected override bool DoEquals(TT x, TT y) => x.ReflectedMember.Equals(y.ReflectedMember);
        }

        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        private static AbstractEqualityComparer<MemberTokenBase<T>> EqualityComparer
        {
            get => Axle.References.Singleton<MethodHandleBaseEqualityComparer<MemberTokenBase<T>>>.Instance;
        }
        #else
        private static readonly AbstractEqualityComparer<MemberTokenBase<T>> comparer 
            = new MethodHandleBaseEqualityComparer<MemberTokenBase<T>>();
        private static AbstractEqualityComparer<MemberTokenBase<T>> EqualityComparer => comparer;
        #endif
    }

    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    abstract partial class MemberTokenBase<T, THandle>
    {
        [System.Serializable]
        private sealed class MethodHandleBaseEqualityComparer : AbstractEqualityComparer<MemberTokenBase<T, THandle>>
        {
            internal MethodHandleBaseEqualityComparer() { }

            protected override int DoGetHashCode(MemberTokenBase<T, THandle> obj) => obj.Handle.GetHashCode();

            protected override bool DoEquals(MemberTokenBase<T, THandle> x, MemberTokenBase<T, THandle> y) 
                => x.Handle.Equals(y.Handle);
        }

        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        private static AbstractEqualityComparer<MemberTokenBase<T, THandle>> EqualityComparer
        {
            get => Axle.References.Singleton<MethodHandleBaseEqualityComparer>.Instance;
        }
        #else
        private static readonly AbstractEqualityComparer<MemberTokenBase<T, THandle>> comparer 
            = new MethodHandleBaseEqualityComparer();
        private static AbstractEqualityComparer<MemberTokenBase<T, THandle>> EqualityComparer => comparer;
        #endif
    }
    #endif
}