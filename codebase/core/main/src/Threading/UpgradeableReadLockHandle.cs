#if NETSTANDARD || NET20_OR_NEWER
using System.Diagnostics;


namespace Axle.Threading
{
    internal sealed class UpgradeableReadLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IReadWriteLock _readWriteLock;

        internal UpgradeableReadLockHandle(IReadWriteLock readWriteLock)
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
#endif