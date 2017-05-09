using System.Diagnostics;


namespace Axle.Threading
{
    internal struct WriteLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IReadWriteLock readWriteLock;

        public WriteLockHandle(IReadWriteLock readWriteLock)
        {
            this.readWriteLock = readWriteLock;
            this.readWriteLock.EnterWriteLock();
        }
        
        public void Dispose()
        {
            if (readWriteLock == null)
            {
                return;
            }
            readWriteLock.ExitWriteLock();
            readWriteLock = null;
        }

        ILock ILockHandle.Lock { get { return readWriteLock; } }
    }
}