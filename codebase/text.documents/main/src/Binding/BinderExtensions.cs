using System;
using Axle.Verification;

namespace Axle.Text.Documents.Binding
{
    public static class BinderExtensions
    {
        public static object Bind(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IDocumentBinder documentBinder,
            ITextDocumentRoot document, Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(documentBinder, nameof(documentBinder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(document, nameof(document)));
            return documentBinder.Bind(new DocumentValueProvider(document), type);
        }

        public static object Bind(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IDocumentBinder documentBinder, 
            ITextDocumentRoot document, object instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(documentBinder, nameof(documentBinder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(document, nameof(document)));
            return documentBinder.Bind(new DocumentValueProvider(document), instance);
        }
    }
}
