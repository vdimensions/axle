using System;
using System.Collections;

namespace Axle.Text.Documents.Binding
{
    public interface IDocumentDictionaryValueAdapter : IDocumentCollectionValueAdapter
    {
        object ItemAt(IDictionary dictionary, object key);
        Type KeyType { get; }
    }
}