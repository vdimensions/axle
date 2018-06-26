//using System.Collections.Generic;
//using System.Linq;
//
//using Axle.Application.IoC.Sdk;
//using Axle.Application.Modularity.Sdk.IoC;
//using Axle.Core.Infrastructure.DependencyInjection;
//
//
//namespace Axle.Application.IoC.Impl
//{
//    internal sealed class DependencyResolutionErrorHandlerChain : AbstractDependencyResolutionErrorHandler, IDependencyResolutionErrorHandlerRegistry
//    {
//        private readonly IList<IDependencyResolutionErrorHandler> chain = new List<IDependencyResolutionErrorHandler>(8);
//
//        public override DependencyResolution ResolveMissingDeclaredDependency(Container container, IDependency dependency)
//        {
//            return chain
//                .Reverse()
//                .Select(x => x.ResolveMissingDeclaredDependency(container, dependency))
//                .FirstOrDefault(x => x.Succeeded) ?? DependencyResolution.Failure;
//        }
//
//        public override DependencyResolution ResolveMissingInferredDependency(Container container, IDependency dependency)
//        {
//            return chain
//                .Reverse()
//                .Select(x => x.ResolveMissingInferredDependency(container, dependency))
//                .FirstOrDefault(x => x.Succeeded) ?? DependencyResolution.Failure;
//        }
//
//        public override DependencyResolution ResolveMultipleCandidatesDeclaredDependency(Container container, IDependency dependency, IEnumerable<IResolutionCandidate> candidates)
//        {
//            return chain
//                .Reverse()
//                .Select(x => x.ResolveMultipleCandidatesDeclaredDependency(container, dependency, candidates))
//                .FirstOrDefault(x => x.Succeeded) ?? DependencyResolution.Failure;
//        }
//
//        public override DependencyResolution ResolveMultipleCandidatesInferredDependency(Container container, IDependency dependency, IEnumerable<IResolutionCandidate> candidates)
//        {
//            return chain
//                .Reverse()
//                .Select(x => x.ResolveMultipleCandidatesInferredDependency(container, dependency, candidates))
//                .FirstOrDefault(x => x.Succeeded) ?? DependencyResolution.Failure;
//        }
//
//        public IDependencyResolutionErrorHandlerRegistry Push(IDependencyResolutionErrorHandler handler)
//        {
//            chain.Add(handler);
//            return this;
//        }
//    }
//}