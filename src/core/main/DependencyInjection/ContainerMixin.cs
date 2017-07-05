using System;

using Axle.Verification;


namespace Axle.DependencyInjection
{
    public static class ContainerMixin
    {
        public static object Resolve(this IContainer container, Type type)
        {
            return container.VerifyArgument(nameof(container)).IsNotNull().Value.Resolve(type, string.Empty);
        }

        public static T Resolve<T>(this IContainer container, string name)
        {
            return (T) container.VerifyArgument(nameof(container)).IsNotNull().Value.Resolve(typeof(T), name.VerifyArgument(nameof(name)).IsNotNull().Value);
        }
        public static T Resolve<T>(this IContainer container)
        {
            return Resolve<T>(container, string.Empty);
        }
    }
}
