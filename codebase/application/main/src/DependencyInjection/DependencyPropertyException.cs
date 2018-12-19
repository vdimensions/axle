using System;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Runtime.Serialization;
#endif

using Axle.Verification;


namespace Axle.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class DependencyPropertyException : DependencyResolutionException
    {
        internal DependencyPropertyException() { }
        private DependencyPropertyException(string message, Exception inner) : base(message, inner) { }
        public DependencyPropertyException(Type type, Type propertyType, string propertyName) : this(type, propertyType, propertyName, null, null) { }
        public DependencyPropertyException(Type type, Type propertyType, string propertyName, Exception inner) : this(type, propertyType, propertyName, null, inner) { }
        public DependencyPropertyException(Type type, Type propertyType, string propertyName, string message, Exception inner) 
            : this(
                string.Format(
                    "Unable to initialize dependency of type `{0}`. Could not satisfy property '{2}' of type `{1}` {3}",
                    type.VerifyArgument(nameof(type)).Value.FullName,
                    propertyType.VerifyArgument(nameof(propertyType)).Value.FullName,
                    propertyName.VerifyArgument(nameof(propertyName)).IsNotNullOrEmpty().Value,
                    $". {message ?? string.Empty}"), 
                inner) { }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected DependencyPropertyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}