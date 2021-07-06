using System.Collections.Generic;

namespace Axle.Data
{
    /// <summary>
    /// An interface representing a registry for <see cref="IDbServiceProvider"/> implementations.
    /// During application initialization, the included <see cref="IDbServiceProvider"/> modules will register
    /// themselves with the application trough this interface.  
    /// </summary>
    public interface IDbServiceProviderRegistry : IEnumerable<IDbServiceProvider>
    {
        // TODO: implement register method and remove the abstract DbServiceProviderModule class 
    }
}
