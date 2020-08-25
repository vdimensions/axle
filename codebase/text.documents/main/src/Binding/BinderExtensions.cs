using System;
using Axle.Verification;

namespace Axle.Text.Documents.Binding
{
    public static class BinderExtensions
    {
        public static object Bind(this IDocumentBinder documentBinder, ITextDocumentRoot document, Type type)
        {
            documentBinder.VerifyArgument(nameof(documentBinder)).IsNotNull();
            document.VerifyArgument(nameof(document)).IsNotNull();
            return documentBinder.Bind(new DocumentValueProvider(document), type);
        }

        public static object Bind(this IDocumentBinder documentBinder, ITextDocumentRoot document, object instance)
        {
            documentBinder.VerifyArgument(nameof(documentBinder)).IsNotNull();
            document.VerifyArgument(nameof(document)).IsNotNull();
            return documentBinder.Bind(new DocumentValueProvider(document), instance);
        }
    }
}
