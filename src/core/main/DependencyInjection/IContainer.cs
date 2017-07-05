using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Axle.DependencyInjection
{
    /// <summary>
    /// An interface representing a dependency container.
    /// </summary>
    public interface IContainer
    {
        object Resolve(Type type, string name);
    }
}
