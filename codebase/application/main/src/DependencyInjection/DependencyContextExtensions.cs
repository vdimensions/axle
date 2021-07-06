using System;

using Axle.Verification;


namespace Axle.DependencyInjection
{
    public static class DependencyContextExtensions
    {
        private sealed class DependencyContextProxy : IDependencyContext
        {
            private readonly IDependencyContext _impl;

            public DependencyContextProxy(IDependencyContext impl)
            {
                _impl = impl;
            }

            object IDependencyContext.Resolve(Type type, string name) => _impl.Resolve(type, name);

            IDependencyContext IDependencyContext.Parent => _impl.Parent;
        }
        
        public static object Resolve(this IDependencyContext context, Type type)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            return context.Resolve(type, string.Empty);
        }
        public static T Resolve<T>(this IDependencyContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            return (T) context.Resolve(typeof(T), name.VerifyArgument(nameof(name)).IsNotNull().Value);
        }
        public static T Resolve<T>(this IDependencyContext context)
        {
            return Resolve<T>(context, string.Empty);
        }

        public static bool TryResolve(this IDependencyContext context, Type type, out object value, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            try
            {
                value = context.Resolve(type, name);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
        public static bool TryResolve(this IDependencyContext context, Type type, out object value)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            try
            {
                value = context.Resolve(type);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
        public static bool TryResolve<T>(this IDependencyContext context, out T value, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            try
            {
                value = context.Resolve<T>(name);
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }
        public static bool TryResolve<T>(this IDependencyContext context, out T value)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            try
            {
                value = context.Resolve<T>();
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }

        public static IDependencyContext AsDependencyContext<T>(this T context) where T : IDependencyContext
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            return new DependencyContextProxy(context);
        }
    }
}
