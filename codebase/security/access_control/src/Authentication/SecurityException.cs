using System;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Runtime.Serialization;
#endif

namespace Axle.Security.AccessControl.Authentication
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class SecurityException : Exception
    {
        public SecurityException(string message, Exception inner = null) : base(message, inner) { }
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected SecurityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}