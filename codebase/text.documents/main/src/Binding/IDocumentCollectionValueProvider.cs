using System.Collections.Generic;

namespace Axle.Text.Documents.Binding
{
    public interface IDocumentCollectionValueProvider : IDocumentValueProvider, IEnumerable<IDocumentValueProvider>
    {
        IDocumentCollectionValueAdapter ValueAdapter { get; }
    }
}
