using System.Diagnostics;


namespace Axle.Threading
{
    internal struct ReadLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IReadWriteLock readWriteLock;

        public ReadLockHandle(IReadWriteLock readWriteLock)
        {
            this.readWriteLock = readWriteLock;
            this.readWriteLock.EnterReadLock();
        }

        public void Dispose()
        {
            if (readWriteLock == null)
            {
                return;
            }
            readWriteLock.ExitReadLock();
            readWriteLock = null;
        }

        ILock ILockHandle.Lock { get { return readWriteLock; } }
    }
}