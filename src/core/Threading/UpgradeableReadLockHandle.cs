using System.Diagnostics;


namespace Axle.Threading
{
    internal sealed class UpgradeableReadLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IReadWriteLock readWriteLock;

        public UpgradeableReadLockHandle(IReadWriteLock readWriteLock)
        {
            (this.readWriteLock = readWriteLock).EnterUpgradeableReadLock();
        }

        public void Dispose()
        {
            if (readWriteLock == null)
            {
                return;
            }
            readWriteLock.ExitUpgradeableReadLock();
            readWriteLock = null;
        }

        ILock ILockHandle.Lock { get { return readWriteLock; } }
    }
}