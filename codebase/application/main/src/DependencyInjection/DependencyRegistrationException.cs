using System;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Runtime.Serialization;
#endif


namespace Axle.Application.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public class DependencyRegistrationException : Exception
    {
        public DependencyRegistrationException() { }
        public DependencyRegistrationException(string message) : base(message) { }
        public DependencyRegistrationException(string message, Exception inner) : base(message, inner) { }
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        protected DependencyRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}