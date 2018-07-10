using System;


namespace Axle.Logging
{
    /// <summary>
    /// An interface for creating <see cref="ILogger">logger</see> implementations.
    /// </summary>
    public interface ILoggingServiceProvider
    {
        /// <summary>
        /// Creates an <see cref="ILogger"/> instance attached to the specified <paramref name="targetType"/>.
        /// </summary>
        /// <param name="targetType">The type the <see cref="ILogger">logger</see> should be attached to.</param>
        /// <returns>
        /// An <see cref="ILogger"/> instance attached to the specified <paramref name="targetType"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="targetType"/> is <c>null</c>.
        /// </exception>
        ILogger Create(Type targetType);

        /// <summary>
        /// Creates an <see cref="ILogger"/> instance attached to the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="ILogger"/> instance attached to the specified type <typeparamref name="T"/>.
        /// </returns>
        /// <typeparam name="T">The type the <see cref="ILogger">logger</see> should be attached to.</typeparam>
        ILogger Create<T>();
    }
}