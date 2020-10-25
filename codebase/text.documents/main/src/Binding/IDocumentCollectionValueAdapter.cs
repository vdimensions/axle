using System;
using System.Collections;

namespace Axle.Text.Documents.Binding
{
    public interface IDocumentCollectionValueAdapter
    {
        object ItemAt(IEnumerable collection, int index);
        object SetItems(object collection, IEnumerable items);
        Type ElementType { get; }
    }
}