using System.Diagnostics;


namespace Axle.Threading
{
    internal sealed class UpgradeableReadLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IReadWriteLock _readWriteLock;

        public UpgradeableReadLockHandle(IReadWriteLock readWriteLock)
        {
            (_readWriteLock = readWriteLock).EnterUpgradeableReadLock();
        }

        public void Dispose()
        {
            if (_readWriteLock == null)
            {
                return;
            }
            _readWriteLock.ExitUpgradeableReadLock();
            _readWriteLock = null;
        }

        ILock ILockHandle.Lock => _readWriteLock;
    }
}