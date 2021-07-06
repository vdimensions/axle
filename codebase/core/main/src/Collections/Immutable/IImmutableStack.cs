using Axle.Collections.ReadOnly;

namespace Axle.Collections.Immutable
{
    /// <summary>
    /// Represents an immutable last-in-first-out (LIFO) collection. 
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the immutable stack.
    /// </typeparam>
    public interface IImmutableStack<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Removes all objects from the immutable stack.
        /// </summary>
        /// <returns>
        /// An empty immutable stack.
        /// </returns>
        IImmutableStack<T> Clear();

        /// <summary>
        /// Inserts an element at the top of the immutable stack and returns the new stack.
        /// </summary>
        /// <param name="value">
        /// The element to push onto the stack.
        /// </param>
        /// <returns>
        /// The new stack.
        /// </returns>
        IImmutableStack<T> Push(T value);

        /// <summary>
        /// Removes the element at the top of the immutable stack and returns the new stack.
        /// </summary>
        /// <returns>
        /// The new stack; never <c>null</c>
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        /// The stack is empty.
        /// </exception>
        IImmutableStack<T> Pop();

        /// <summary>
        /// Removes the specified element from the immutable stack and returns the stack after the removal.
        /// </summary>
        /// <param name="value">
        /// The value to remove from the stack.
        /// </param>
        /// <returns>
        /// The new stack; never <c>null</c>
        /// </returns>
        IImmutableStack<T> Pop(out T value);

        /// <summary>
        /// Returns the element at the top of the immutable stack without removing it.
        /// </summary>
        /// <returns>
        /// The element at the top of the stack.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        /// The stack is empty.
        /// </exception>
        T Peek();

        /// <summary>
        /// Gets a value that indicates whether this immutable stack is empty.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if this stack is empty; otherwise,<see langword="false" />.
        /// </returns>
        bool IsEmpty { get; }
    }
}