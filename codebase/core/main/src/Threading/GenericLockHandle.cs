using System;

namespace Axle.Threading
{
    /// <summary>
    /// An implementation of the <see cref="ILockHandle"/> interface that is agnostic of the target lock's
    /// implementation.
    /// </summary>
    internal sealed class GenericLockHandle : ILockHandle
    {
        private readonly ILock _lock;

        internal GenericLockHandle(ILock @lock)
        {
            _lock = @lock;
            @lock.Enter();
        }

        /// <inheritdoc cref="IDisposable.Dispose()"/>
        public void Dispose() => _lock?.Exit();
        void IDisposable.Dispose() => Dispose();
    }
}