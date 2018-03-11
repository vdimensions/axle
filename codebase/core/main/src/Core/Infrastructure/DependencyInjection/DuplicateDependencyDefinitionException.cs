using System;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Runtime.Serialization;
#endif


namespace Axle.Core.Infrastructure.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public class DuplicateDependencyDefinitionException : DependencyRegistrationException
    {
        public DuplicateDependencyDefinitionException() { }
        public DuplicateDependencyDefinitionException(string message) : base(message) { }
        public DuplicateDependencyDefinitionException(string message, Exception inner) : base(message, inner) { }
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        protected DuplicateDependencyDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}