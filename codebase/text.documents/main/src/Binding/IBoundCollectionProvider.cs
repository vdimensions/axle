using System.Collections.Generic;

namespace Axle.Text.Documents.Binding
{
    public interface IBoundCollectionProvider : IBoundValueProvider, IEnumerable<IBoundValueProvider>
    {
        IBindingCollectionAdapter CollectionAdapter { get; }
    }
}
