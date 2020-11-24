using System;

namespace Axle.Threading
{
    public abstract class AbstractReadWriteLockProvider
    {
        protected AbstractReadWriteLockProvider()
        {
            ReadLock = new ReadLock(this);
            UpgradeableReadLock = new UpgradeableReadLock(this);
            WriteLock = new WriteLock(this);
        }
        public abstract void EnterReadLock();
        
        public abstract void EnterUpgradeableReadLock();
        
        public abstract void EnterWriteLock();
        
        public abstract void ExitReadLock();
        
        public abstract void ExitUpgradeableReadLock();
        
        public abstract void ExitWriteLock();
        
        public abstract bool TryEnterReadLock(int millisecondsTimeout);
        public abstract bool TryEnterReadLock(TimeSpan timeout);
        
        public abstract bool TryEnterUpgradeableReadLock(int millisecondsTimeout);
        public abstract bool TryEnterUpgradeableReadLock(TimeSpan timeout);
        
        public abstract bool TryEnterWriteLock(int millisecondsTimeout);
        public abstract bool TryEnterWriteLock(TimeSpan timeout);
        
        
        public ReadLock ReadLock { get; }

        public UpgradeableReadLock UpgradeableReadLock { get; }

        public WriteLock WriteLock { get; } 
    }
}