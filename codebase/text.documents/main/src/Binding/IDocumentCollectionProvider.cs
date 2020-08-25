using System.Collections.Generic;

namespace Axle.Text.Documents.Binding
{
    public interface IDocumentCollectionProvider : IDocumentValueProvider, IEnumerable<IDocumentValueProvider>
    {
        IDocumentCollectionValueAdapter CollectionValueAdapter { get; }
    }
}
