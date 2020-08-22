using System;
using System.Collections.Generic;

namespace Axle.Caching
{
    public interface ICacheManager : IDisposable
    {
        ICache GetCache(string name);

        void Invalidate(string name);
        void InvalidateAll();
        
        IEnumerable<string> Caches { get; }
    }
}