//using System.Collections.Generic;
//
//using Axle.Application.IoC;
//using Axle.Application.IoC.Sdk;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
//{
//    public abstract class AbstractDependencyResolutionHandler : IDependencyResolutionErrorHandler
//    {
//        public virtual DependencyResolution ResolveMissingDeclaredDependency(Container container, IDependency dependency)
//        {
//            return DependencyResolution.Failure;
//        }
//
//        public virtual DependencyResolution ResolveMissingInferredDependency(Container container, IDependency dependency)
//        {
//            return DependencyResolution.Failure;
//        }
//
//        public virtual DependencyResolution ResolveMultipleCandidatesDeclaredDependency(Container container, IDependency dependency, IEnumerable<IResolutionCandidate> candidates)
//        {
//            return DependencyResolution.Failure;
//        }
//
//        public virtual DependencyResolution ResolveMultipleCandidatesInferredDependency(Container container, IDependency dependency, IEnumerable<IResolutionCandidate> candidates)
//        {
//            return DependencyResolution.Failure;
//        }
//    }
//}