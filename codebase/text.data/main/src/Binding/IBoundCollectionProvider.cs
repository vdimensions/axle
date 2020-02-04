using System.Collections.Generic;

namespace Axle.Text.Data.Binding
{
    public interface IBoundCollectionProvider : IBoundValueProvider, IEnumerable<IBoundValueProvider>
    {
        IBindingCollectionAdapter CollectionAdapter { get; }
    }
}
