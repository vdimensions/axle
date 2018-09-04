using System;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Runtime.Serialization;
#endif


namespace Axle.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class DependencyNotFoundException : DependencyResolutionException
    {
        public DependencyNotFoundException() { }
        [Obsolete]
        public DependencyNotFoundException(string message) : base(message) { }
        [Obsolete]
        public DependencyNotFoundException(string message, Exception inner) : base(message, inner) { }
        public DependencyNotFoundException(Type type, string name) : this(type, name, null) { }
        public DependencyNotFoundException(Type type, string name, Exception inner) : base(type, name, "Could not find suitable candidate to inject.", inner) { }
        //[Obsolete]
        //public DependencyNotFoundException(Type type, string name, IDependency dependency, Exception inner) : base(
        //    type, 
        //    string.Format(
        //        "Unable to resolve dependency {0}of type {1}. Could not find suitable candidate to inject for dependency '{2}' of type {3}.", 
        //        string.IsNullOrEmpty(name) ? string.Empty : string.Format("'{0}' ", name), 
        //        type.FullName,
        //        dependency.DeclaredName ?? dependency.InferredName,
        //        dependency.RequestedType), 
        //    inner) { }
        //public DependencyNotFoundException(Type type, string name, Exception inner) : base(type, name, inner) { }
        //public DependencyNotFoundException(Type type, string name, string message, Exception inner) : base(type, name, message, inner) { }
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected DependencyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}