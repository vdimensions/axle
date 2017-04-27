using System;


namespace Axle.References
{
    [Serializable]
    public class WeakReference<T> : WeakReference, 
        IWeakReference<T>, 
        IEquatable<WeakReference<T>>, 
        IEquatable<IWeakReference<T>> 
        where T: class
    {
        public WeakReference(T target, bool trackResurrection) : base(target, trackResurrection) { }
        public WeakReference(T target) : base(target) { }

        private bool DoEquals(IWeakReference<T> other)
        {
            var xTarget = Target;
            var yTarget = other.Value;

            return !(ReferenceEquals(xTarget, null) || ReferenceEquals(yTarget, null)) && (ReferenceEquals(xTarget, yTarget) || Equals(xTarget, yTarget));
        }
        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) || DoEquals(obj as WeakReference<T>));
        }
        public bool Equals(WeakReference<T> other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || DoEquals(other));
        }
        bool IEquatable<IWeakReference<T>>.Equals(IWeakReference<T> other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || DoEquals(other));
        }

        public override int GetHashCode() { return base.GetHashCode(); }

        public T Value
        {
            get { return (T) base.Target; }
            set { base.Target = value; }
        }
        T IReference<T>.Value { get { return Value; } }
        object IReference.Value { get { return Value; } }
    }
}
