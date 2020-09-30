//using System;
//using System.Collections.Generic;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
//{
//    public interface IKernel : IDisposable
//    {
//        IEnumerable<IDependencyReference> GetDependencies();
//        IEnumerable<IDependencyReference> GetDependencies(Type type);
//
//        void RegisterSingleton(Type type, string name, params string[] aliases);
//
//        void RegisterPrototype(Type type, string name, params string[] aliases);
//
//        void RegisterInstance(object instance, string name, params string[] aliases);
//
//        //bool TryResolve(Container container, Type type, string name, out object instance, out DependencyResolutionException ex);
//
//        IKernel Parent { get; }
//        IKernel Root { get; }
//    }
//}