#if NETSTANDARD || NET20_OR_NEWER
using System.Diagnostics;


namespace Axle.Threading
{
    internal struct WriteLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IReadWriteLock _readWriteLock;

        public WriteLockHandle(IReadWriteLock readWriteLock)
        {
            _readWriteLock = readWriteLock;
            _readWriteLock.EnterWriteLock();
        }
        
        public void Dispose()
        {
            if (_readWriteLock == null)
            {
                return;
            }
            _readWriteLock.ExitWriteLock();
            _readWriteLock = null;
        }

        ILock ILockHandle.Lock => _readWriteLock;
    }
}
#endif