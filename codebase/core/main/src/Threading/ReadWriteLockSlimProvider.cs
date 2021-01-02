#if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Diagnostics;
using System.Threading;

namespace Axle.Threading
{
    internal sealed class ReadWriteLockSlimProvider : AbstractReadWriteLockProvider, IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ReaderWriterLockSlim _innerLock;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool _supportsRecursion;

        public ReadWriteLockSlimProvider(bool supportsRecursion)
        {
            _innerLock = new ReaderWriterLockSlim(supportsRecursion ? LockRecursionPolicy.SupportsRecursion : LockRecursionPolicy.NoRecursion);
            _supportsRecursion = supportsRecursion;
        }

        public override void EnterReadLock()
        {
            _innerLock.EnterReadLock();
        }

        public override void EnterUpgradeableReadLock()
        {
            _innerLock.EnterUpgradeableReadLock();
        }

        public override void EnterWriteLock()
        {
            _innerLock.EnterWriteLock();
        }

        public override void ExitReadLock()
        {
            _innerLock.ExitReadLock();
        }

        public override void ExitUpgradeableReadLock()
        {
            _innerLock.ExitUpgradeableReadLock();
        }

        public override void ExitWriteLock()
        {
            _innerLock.ExitWriteLock();
        }

        public override bool TryEnterReadLock(int millisecondsTimeout)
        {
            return _innerLock.TryEnterReadLock(millisecondsTimeout);
        }

        public override bool TryEnterReadLock(TimeSpan timeout)
        {
            return _innerLock.TryEnterReadLock(timeout);
        }

        public override bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
        {
            return _innerLock.TryEnterUpgradeableReadLock(millisecondsTimeout);
        }

        public override bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            return _innerLock.TryEnterUpgradeableReadLock(timeout);
        }

        public override bool TryEnterWriteLock(int millisecondsTimeout)
        {
            return _innerLock.TryEnterWriteLock(millisecondsTimeout);
        }

        public override bool TryEnterWriteLock(TimeSpan timeout)
        {
            return _innerLock.TryEnterWriteLock(timeout);
        }

        void IDisposable.Dispose() => _innerLock?.Dispose();

        public bool SupportsRecursion => _supportsRecursion;
    }
}
#endif