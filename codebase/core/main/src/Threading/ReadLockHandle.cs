using System.Diagnostics;


namespace Axle.Threading
{
    internal struct ReadLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IReadWriteLock _readWriteLock;

        public ReadLockHandle(IReadWriteLock readWriteLock)
        {
            _readWriteLock = readWriteLock;
            _readWriteLock.EnterReadLock();
        }

        public void Dispose()
        {
            if (_readWriteLock == null)
            {
                return;
            }
            _readWriteLock.ExitReadLock();
            _readWriteLock = null;
        }

        ILock ILockHandle.Lock => _readWriteLock;
    }
}