using System;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Runtime.Serialization;
#endif

using Axle.Verification;


namespace Axle.Core.Infrastructure.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public class DependencyPropertyException : DependencyResolutionException
    {
        internal DependencyPropertyException() { }
        private DependencyPropertyException(string message, Exception inner) : base(message, inner) { }
        public DependencyPropertyException(Type type, string propertyName) : this(type, propertyName, null, null) { }
        public DependencyPropertyException(Type type, string propertyName, Exception inner) : this(type, propertyName, null, inner) { }
        public DependencyPropertyException(Type type, string propertyName, string message, Exception inner) 
            : this(
                string.Format(
                    "Unable to initialize dependency of type {0}. Could not satisfy property '{1}'{2}",
                    type.VerifyArgument(nameof(type)).Value.FullName,
                    propertyName.VerifyArgument(nameof(propertyName)).IsNotNullOrEmpty().Value,
                    $". {message ?? string.Empty}"), 
                inner) { }

        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        protected DependencyPropertyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}