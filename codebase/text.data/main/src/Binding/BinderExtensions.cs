using Axle.Verification;
using System;

namespace Axle.Text.Data.Binding
{
    public static class BinderExtensions
    {
        public static object Bind(this IBinder binder, ITextDataRoot data, Type type)
        {
            binder.VerifyArgument(nameof(binder)).IsNotNull();
            data.VerifyArgument(nameof(data)).IsNotNull();
            return binder.Bind(new TextDataProvider(data), type);
        }

        public static object Bind(this IBinder binder, ITextDataRoot data, object instance)
        {
            binder.VerifyArgument(nameof(binder)).IsNotNull();
            data.VerifyArgument(nameof(data)).IsNotNull();
            return binder.Bind(new TextDataProvider(data), instance);
        }
    }
}
