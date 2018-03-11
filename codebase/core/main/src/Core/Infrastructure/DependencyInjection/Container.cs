using System;
using System.Collections.Generic;
using System.Diagnostics;

using Axle.Core.Infrastructure.DependencyInjection;
using Axle.Verification;


namespace Axle.Core.Infrastructure.DependencyInjection
{
    public partial class Container : IDisposable
    {
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly IKernel kernel;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly Container parent;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly Container root;
//
//        public Container() : this(new DefaultKernel()) { }
//        public Container(Container parent)
//        {
//            this.kernel = new DefaultKernel(parent.VerifyArgument("parent").IsNotNull().Value.kernel);
//            this.parent = parent;
//            this.root = parent.Root;
//        }
//        internal Container(IKernel kernel)
//        {
//            this.kernel = kernel;
//            this.parent = kernel.Parent == null ? null : new Container(kernel.Parent);
//            this.root = parent == null ? this : parent.root;
//        }
//
        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            //if (kernel != null)
            //{
            //    kernel.Dispose();
            //}
        }

        public void Dispose() => Dispose(true);
        void IDisposable.Dispose() => Dispose(true);
//
//        public IEnumerable<IDependencyReference> GetDependencies() { return kernel.GetDependencies(); }
//        public IEnumerable<IDependencyReference> GetDependencies(Type type) { return kernel.GetDependencies(type); }
//
//        public Container Register(Type type) { return Register(type, string.Empty); }
//        public Container Register(Type type, string name, params string[] aliases)
//        {
//            return StatelessAttribute.IsStateless(type).GetValueOrDefault(true) 
//                ? RegisterSingleton(type, name ?? string.Empty, aliases)
//                : RegisterPrototype(type, name ?? string.Empty, aliases);
//        }
//        public Container Register<T>() { return Register<T>(string.Empty); }
//        public Container Register<T>(string name, params string[] aliases)
//        {
//            return StatelessAttribute.IsStateless<T>().GetValueOrDefault(true)
//                ? RegisterSingleton<T>(name ?? string.Empty, aliases)
//                : RegisterPrototype<T>(name ?? string.Empty, aliases);
//        }
//
//        public Container RegisterSingleton(Type type) { return RegisterSingleton(type, string.Empty); }
//        public Container RegisterSingleton(Type type, string name, params string[] aliases)
//        {
//            kernel.RegisterSingleton(type, name ?? string.Empty, aliases);
//            return this;
//        }
//        public Container RegisterSingleton<T>() { return RegisterSingleton(typeof(T)); }
//        public Container RegisterSingleton<T>(string name, params string[] aliases) { return RegisterSingleton(typeof(T), name, aliases); }
//
//        public Container RegisterPrototype(Type type) { return RegisterPrototype(type, string.Empty); }
//        public Container RegisterPrototype(Type type, string name, params string[] aliases)
//        {
//            kernel.RegisterPrototype(type, name ?? string.Empty, aliases);
//            return this;
//        }
//        public Container RegisterPrototype<T>() { return RegisterPrototype(typeof(T)); }
//        public Container RegisterPrototype<T>(string name, params string[] aliases) { return RegisterPrototype(typeof(T), name, aliases); }
//
//        public Container RegisterInstance(object instance) { return RegisterInstance(instance, string.Empty); }
//        public Container RegisterInstance(object instance, string name, params string[] aliases)
//        {
//            kernel.RegisterInstance(instance, name ?? string.Empty, aliases);
//            return this;
//        }
//        public Container RegisterInstance<T>(T instance) { return RegisterInstance(instance, string.Empty); }
//        public Container RegisterInstance<T>(T instance, string name, params string[] aliases)
//        {
//            kernel.RegisterInstance(instance, name ?? string.Empty);
//            return this;
//        }
//
//        public object Resolve(Type type) { return Resolve(type, string.Empty); }
//        public object Resolve(Type type, string name)
//        {
//            DependencyResolutionException exception;
//            object instance;
//            if (kernel.TryResolve(this, type, name, out instance, out exception))
//            {
//                return instance;
//            }
//            throw exception;
//        }
//        public T Resolve<T>() { return (T) Resolve(typeof(T), string.Empty); }
//        public T Resolve<T>(string name) { return (T) Resolve(typeof (T), name); }
//
//        public object ResolveOnce(Type type) { return ResolveOnce(type, String.Empty); }
//        public object ResolveOnce(Type type, string name)
//        {
//            using (var temp = new Container(this))
//            {
//                return temp.RegisterPrototype(type, name).Resolve(type, name);
//            }
//        }
//        public T ResolveOnce<T>() { return (T) ResolveOnce(typeof(T), String.Empty); }
//        public T ResolveOnce<T>(string name) { return (T) ResolveOnce(typeof(T), name); }
//
//        public bool TryResolve(Type type, out object instance) { return TryResolve(type, string.Empty, out instance); }
//        public bool TryResolve(Type type, string name, out object instance)
//        {
//            DependencyResolutionException unused;
//            if (kernel.TryResolve(this, type, name, out instance, out unused))
//            {
//                return true;
//            }
//            instance = null;
//            return false;
//        }
//        public bool TryResolve<T>(out T instance) { return TryResolve(string.Empty, out instance); }
//        public bool TryResolve<T>(string name, out T instance)
//        {
//            object instance1;
//            if (TryResolve(typeof(T), name, out instance1))
//            {
//                instance = (T) instance1;
//                return true;
//            }
//            instance = default(T);
//            return false;
//        }
//
//        public bool TryResolveOnce(Type type, out object instance) { return TryResolveOnce(type, string.Empty, out instance); }
//        public bool TryResolveOnce(Type type, string name, out object instance)
//        {
//            using (var temp = new Container(this))
//            {
//                return temp.RegisterPrototype(type, name).TryResolve(type, name, out instance);
//            }
//        }
//        public bool TryResolveOnce<T>(out T instance) { return TryResolveOnce(string.Empty, out instance); }
//        public bool TryResolveOnce<T>(string name, out T instance)
//        {
//            using (var temp = new Container(this))
//            {
//                return temp.RegisterPrototype<T>(name).TryResolve(name, out instance);
//            }
//        }
//
//        public Container Parent { get { return parent; } }
//        public Container Root { get { return root; } }
    }
}