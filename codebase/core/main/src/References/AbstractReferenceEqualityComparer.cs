using System.Collections.Generic;

namespace Axle.References
{
    /// <summary>
    /// An abstract class that provides default implementation for comparing <see cref="IReference{T}"/>
    /// implementations.
    /// </summary>
    /// <typeparam name="TRef">
    /// The type of the reference object.
    /// </typeparam>
    /// <typeparam name="T">
    /// The type of the reference's underlying object.
    /// </typeparam>
    public abstract class AbstractReferenceEqualityComparer<T, TRef> : AbstractEqualityComparer<TRef>
        where TRef : IReference<T>
    {
        /// <summary>
        /// When called from a derived class constructor, initializes the current
        /// <see cref="AbstractReferenceEqualityComparer{T, T}"/> implementation with a default comparer for the
        /// for checking the equality of the underlying reference values.
        /// </summary>
        protected AbstractReferenceEqualityComparer() : this(EqualityComparer<T>.Default) { }
        /// <summary>
        /// When called from a derived class constructor, initializes the current
        /// <see cref="AbstractReferenceEqualityComparer{T, T}"/> implementation with the provided
        /// <paramref name="valueComparer"/> for checking the equality of the underlying reference values.
        /// </summary>
        protected AbstractReferenceEqualityComparer(IEqualityComparer<T> valueComparer)
        {
            ValueComparer = valueComparer;
        }

        /// <inheritdoc />
        protected override bool DoEquals(TRef x, TRef y) 
            => x.TryGetValue(out var vx) & y.TryGetValue(out var vy) && ValueComparer.Equals(vx, vy);

        /// <inheritdoc />
        protected override int DoGetHashCode(TRef obj) 
            => obj.TryGetValue(out var v) ? ValueComparer.GetHashCode(v) : 0;

        /// <summary>
        /// Gets a reference to the <see cref="IEqualityComparer{T}"/> that is used to compare the resolved reference
        /// values.
        /// </summary>
        /// <value>The value comparer.</value>
        public IEqualityComparer<T> ValueComparer { get; }
    }
}