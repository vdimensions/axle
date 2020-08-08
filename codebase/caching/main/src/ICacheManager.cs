using System.Collections.Generic;

namespace Axle.Caching
{
    public interface ICacheManager
    {
        ICache GetCache(string name);

        void Invalidate(string name);
        
        IEnumerable<string> Caches { get; }
    }
}