#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.References
{
    /// <inheritdoc cref="IWeakReference{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class WeakRef<T> : IWeakReference<T>, IEquatable<WeakRef<T>>, IEquatable<IWeakReference<T>> where T: class
    {
        #if NETSTANDARD || NET45_OR_NEWER
        private readonly WeakReference<T> _reference;

        private WeakRef(WeakReference<T> reference) { _reference = reference; }
        public WeakRef(T target, bool trackResurrection) : this(new WeakReference<T>(target, trackResurrection)) { }
        public WeakRef(T target) : this(new WeakReference<T>(target)) { }
        #else
        private readonly WeakReference _reference;

        private WeakRef(WeakReference reference) { _reference = reference; }
        public WeakRef(T target, bool trackResurrection) : this(new WeakReference(target, trackResurrection)) { }
        public WeakRef(T target) : this(new WeakReference(target)) { }
        #endif

        private bool DoEquals(IWeakReference<T> other)
        {
            var xTarget = Value;
            var yTarget = other.Value;

            return !(ReferenceEquals(xTarget, null) || ReferenceEquals(yTarget, null)) && (ReferenceEquals(xTarget, yTarget) || Equals(xTarget, yTarget));
        }

        public override bool Equals(object obj) => !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) || DoEquals(obj as WeakRef<T>));
        public bool Equals(WeakRef<T> other) => !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || DoEquals(other));
        bool IEquatable<IWeakReference<T>>.Equals(IWeakReference<T> other) => !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || DoEquals(other));

        public override int GetHashCode() => _reference.GetHashCode();

        public bool TryGetValue(out T target)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            return _reference.TryGetTarget(out target);
            #else
            target = Value;
            return IsAlive;
            #endif
        }

        public T Value
        {
            get
            {
                #if NETSTANDARD || NET45_OR_NEWER
                return _reference.TryGetTarget(out var xTarget) ? xTarget : null;
                #else
                return (T) _reference.Target;
                #endif
            }
            set
            {
                #if NETSTANDARD || NET45_OR_NEWER
                _reference.SetTarget(value);
                #else
                _reference.Target = value;
                #endif
            }
        }

        /// <inheritdoc cref="IWeakReference{T}.Value" />
        T IReference<T>.Value => Value;

        /// <inheritdoc cref="IWeakReference{T}.Value" />
        object IReference.Value => Value;

        public bool IsAlive
        {
            get
            {
                #if NETSTANDARD || NET45_OR_NEWER
                return _reference.TryGetTarget(out _);
                #else
                return _reference.IsAlive;
                #endif
            }
        }

        /// <inheritdoc cref="IWeakReference{T}.IsAlive" />
        bool IWeakReference<T>.IsAlive => IsAlive;
    }
}
#endif