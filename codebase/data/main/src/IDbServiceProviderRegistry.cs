using System.Collections.Generic;

namespace Axle.Data
{
    internal interface IDbServiceProviderRegistry : IEnumerable<IDbServiceProvider>
    {
        IDbServiceProvider this[string providerName] { get; }
    }
}
