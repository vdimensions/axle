using System.Diagnostics;

namespace Axle.Threading
{
    internal struct ReadLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AbstractReadWriteLockProvider _readWriteLockProvider;

        internal ReadLockHandle(AbstractReadWriteLockProvider readWriteLockProvider)
        {
            _readWriteLockProvider = readWriteLockProvider;
            _readWriteLockProvider.EnterReadLock();
        }

        public void Dispose()
        {
            if (_readWriteLockProvider == null)
            {
                return;
            }
            _readWriteLockProvider.ExitReadLock();
            _readWriteLockProvider = null;
        }
    }
}