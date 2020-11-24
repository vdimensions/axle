using System.Diagnostics;
using System.Threading;

namespace Axle.Threading
{
    internal struct WriteLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AbstractReadWriteLockProvider _readWriteLockProvider;

        internal WriteLockHandle(AbstractReadWriteLockProvider readWriteLockProvider)
        {
            _readWriteLockProvider = readWriteLockProvider;
            _readWriteLockProvider.EnterWriteLock();
        }

        public void Dispose()
        {
            if (_readWriteLockProvider == null)
            {
                return;
            }
            _readWriteLockProvider.ExitWriteLock();
            _readWriteLockProvider = null;
        }
    }
}