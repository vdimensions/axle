namespace Axle.Threading
{
    public sealed class ReentrantReadWriteLock : ReadWriteLock, IReentrantReadWriteLock
    {
        public ReentrantReadWriteLock() : base(true) {}
    }
}