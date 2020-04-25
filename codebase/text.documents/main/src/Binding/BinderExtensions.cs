using System;
using Axle.Verification;

namespace Axle.Text.Documents.Binding
{
    public static class BinderExtensions
    {
        public static object Bind(this IBinder binder, ITextDocumentRoot document, Type type)
        {
            binder.VerifyArgument(nameof(binder)).IsNotNull();
            document.VerifyArgument(nameof(document)).IsNotNull();
            return binder.Bind(new TextDataProvider(document), type);
        }

        public static object Bind(this IBinder binder, ITextDocumentRoot document, object instance)
        {
            binder.VerifyArgument(nameof(binder)).IsNotNull();
            document.VerifyArgument(nameof(document)).IsNotNull();
            return binder.Bind(new TextDataProvider(document), instance);
        }
    }
}
