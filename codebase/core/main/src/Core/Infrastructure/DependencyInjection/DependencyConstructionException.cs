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
    public class DependencyConstructionException : DependencyResolutionException
    {
        public DependencyConstructionException() : base() { }
        public DependencyConstructionException(bool isRecoverable) : base(isRecoverable) { }
        public DependencyConstructionException(string message) : base(message) { }
        public DependencyConstructionException(string message, bool isRecoverable) : base(message, isRecoverable) { }
        public DependencyConstructionException(string message, Exception inner) : base(message, inner) { }
        public DependencyConstructionException(string message, Exception inner, bool isRecoverable) : base(message, inner, isRecoverable) { }
        public DependencyConstructionException(Type type, string argName, Exception inner) : this(type, argName, null, inner) { }
        public DependencyConstructionException(Type type, string argName, Exception inner, bool isRecoverable) : this(type, argName, null, inner, isRecoverable) { }
        public DependencyConstructionException(Type type, string argName, string message, Exception inner) : this(type, argName, message, inner, true) { }
        public DependencyConstructionException(Type type, string argName, string message, Exception inner, bool isRecoverable) : this(
            string.Format(
                "Unable to initialize dependency of type {0}. Could not satisfy constructor argument {1}{2}",
                type.VerifyArgument("type").Value.FullName,
                argName,
                message == null ? string.Empty : string.Format(". {0}", message)), 
            inner,
            isRecoverable) { }
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        /// <inheritdoc />
        protected DependencyConstructionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}