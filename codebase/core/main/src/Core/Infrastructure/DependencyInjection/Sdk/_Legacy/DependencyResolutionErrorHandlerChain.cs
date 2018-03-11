//using System;
//using System.Collections.Generic;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
//{
//    [Serializable]
//    public abstract class AbstractDependencyResolutionErrorHandler : IDependencyResolutionErrorHandler
//    {
//        protected AbstractDependencyResolutionErrorHandler() { }
//
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