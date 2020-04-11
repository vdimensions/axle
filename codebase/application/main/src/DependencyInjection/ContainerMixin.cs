using System;

using Axle.Verification;


namespace Axle.DependencyInjection
{
    public static class ContainerMixin
    {
        public static IDependencyContainer RegisterInstance(this IDependencyContainer dependencyContainer, object instance)
        {
            return dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.RegisterInstance(instance, string.Empty);
        }
        public static IDependencyContainer RegisterInstance<T>(this IDependencyContainer dependencyContainer, T instance, string name, params string[] aliases)
        {
            return dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.RegisterInstance(instance, name, aliases);
        }
        public static IDependencyContainer RegisterInstance<T>(this IDependencyContainer dependencyContainer, T instance)
        {
            return dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.RegisterInstance(instance, string.Empty);
        }

        public static IDependencyContainer RegisterType(this IDependencyContainer dependencyContainer, Type type)
        {
            return dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.RegisterType(type, string.Empty);
        }
        public static IDependencyContainer RegisterType<T>(this IDependencyContainer dependencyContainer, string name, params string[] aliases)
        {
            return dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.RegisterType(typeof(T), name, aliases);
        }
        public static IDependencyContainer RegisterType<T>(this IDependencyContainer dependencyContainer)
        {
            return dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.RegisterType(typeof(T), string.Empty);
        }

        public static object Resolve(this IDependencyContainer dependencyContainer, Type type)
        {
            return dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.Resolve(type, string.Empty);
        }
        public static T Resolve<T>(this IDependencyContainer dependencyContainer, string name)
        {
            return (T) dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.Resolve(typeof(T), name.VerifyArgument(nameof(name)).IsNotNull().Value);
        }
        public static T Resolve<T>(this IDependencyContainer dependencyContainer)
        {
            return Resolve<T>(dependencyContainer, string.Empty);
        }

        public static bool TryResolve(this IDependencyContainer dependencyContainer, Type type, out object value, string name)
        {
            try
            {
                value = dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.Resolve(type, name);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
        public static bool TryResolve(this IDependencyContainer dependencyContainer, Type type, out object value)
        {
            try
            {
                value = dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.Resolve(type);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
        public static bool TryResolve<T>(this IDependencyContainer dependencyContainer, out T value, string name)
        {
            try
            {
                value = dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.Resolve<T>(name);
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }
        public static bool TryResolve<T>(this IDependencyContainer dependencyContainer, out T value)
        {
            try
            {
                value = dependencyContainer.VerifyArgument(nameof(dependencyContainer)).IsNotNull().Value.Resolve<T>();
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }
    }
}
