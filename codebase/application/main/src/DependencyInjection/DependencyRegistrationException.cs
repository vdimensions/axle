using System;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System.Runtime.Serialization;
#endif


namespace Axle.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public class DependencyRegistrationException : Exception
    {
        public DependencyRegistrationException() { }
        public DependencyRegistrationException(string message) : base(message) { }
        public DependencyRegistrationException(string message, Exception inner) : base(message, inner) { }
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        protected DependencyRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}