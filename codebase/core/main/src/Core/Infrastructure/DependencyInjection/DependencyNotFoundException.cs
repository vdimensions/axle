using System;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Runtime.Serialization;
#endif

using Axle.Core.Infrastructure.DependencyInjection.Sdk;


namespace Axle.Core.Infrastructure.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public class DependencyNotFoundException : DependencyResolutionException
    {
        public DependencyNotFoundException() { }
        public DependencyNotFoundException(string message) : base(message) { }
        public DependencyNotFoundException(string message, Exception inner) : base(message, inner) { }
        public DependencyNotFoundException(Type type, string name, IDependency dependency, Exception inner) : base(
            type, 
            name, 
            string.Format(
                "Unable to resolve dependency {0}of type {1}. Could not find suitable candidate to inject for dependency '{2}' of type {3}.", 
                string.IsNullOrEmpty(name) ? string.Empty : string.Format("'{0}' ", name), 
                type.FullName,
                dependency.DeclaredName ?? dependency.InferredName,
                dependency.RequestedType), 
            inner) { }
        //public DependencyNotFoundException(Type type, string name, Exception inner) : base(type, name, inner) { }
        //public DependencyNotFoundException(Type type, string name, string message, Exception inner) : base(type, name, message, inner) { }
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        protected DependencyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}