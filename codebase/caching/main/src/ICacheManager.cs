namespace Axle.Caching
{
    public interface ICacheManager
    {
        ICache GetCache(string name);

        void Invalidate(string name);
    }
}