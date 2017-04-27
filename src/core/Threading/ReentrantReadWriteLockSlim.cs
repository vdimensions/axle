namespace Axle.Threading
{
    public sealed class ReentrantReadWriteLockSlim : ReadWriteLockSlim, IReentrantReadWriteLock
    {
        public ReentrantReadWriteLockSlim() : base(true) {}
    }
}