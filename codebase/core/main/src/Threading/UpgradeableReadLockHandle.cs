#if NETSTANDARD || NET20_OR_NEWER || (UNITY_2018_1_OR_NEWER && (UNITY_EDITOR || !UNITY_WEBGL))
using System.Diagnostics;

namespace Axle.Threading
{
    internal sealed class UpgradeableReadLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AbstractReadWriteLockProvider _readWriteLockProvider;

        internal UpgradeableReadLockHandle(AbstractReadWriteLockProvider readWriteLockProvider)
        {
            (_readWriteLockProvider = readWriteLockProvider).EnterUpgradeableReadLock();
        }

        public void Dispose()
        {
            if (_readWriteLockProvider == null)
            {
                return;
            }
            _readWriteLockProvider.ExitUpgradeableReadLock();
            _readWriteLockProvider = null;
        }
    }
}
#endif
