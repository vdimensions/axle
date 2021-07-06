using System;

namespace Axle.Text.Documents.Binding
{
    internal static class DocumentValueProvider
    {
        public static IDocumentValueProvider Get(ITextDocumentNode node)
        {
            switch (node)
            {
                case ITextDocumentValue v:
                    return new DocumentSimpleValueProvider(v.Key, v.Value);
                case ITextDocumentObject o:
                    return new DocumentComplexValueProvider(o);
            }
            throw new NotSupportedException($"Unsupported node type {node.GetType().FullName}");
        }

    }
}
