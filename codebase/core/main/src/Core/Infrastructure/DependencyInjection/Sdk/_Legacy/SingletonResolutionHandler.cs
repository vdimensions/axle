//using System.Diagnostics;
//
//using Axle.Application.IoC.Sdk;
//using Axle.Verification;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
//{
//    public sealed class SingletonResolutionHandler<T> : AbstractDependencyResolutionHandler
//    {
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly T instance;
//        
//        public SingletonResolutionHandler(T instance)
//        {
//            this.instance = instance.VerifyArgument("instance").IsNotNull().Value;
//        }
//
//        public override DependencyResolution ResolveMissingDeclaredDependency(Container container, IDependency dependency)
//        {
//            if (dependency.RequestedType == typeof(T))
//            {
//                return DependencyResolution.Singleton(instance);
//            }
//            return base.ResolveMissingDeclaredDependency(container, dependency);
//        }
//
//        public override DependencyResolution ResolveMissingInferredDependency(Container container, IDependency dependency)
//        {
//            if (dependency.RequestedType == typeof(T))
//            {
//                return DependencyResolution.Singleton(instance);
//            }
//            return base.ResolveMissingInferredDependency(container, dependency);
//        }
//    }
//}