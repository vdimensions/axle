using System;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Runtime.Serialization;
#endif

using Axle.Verification;


namespace Axle.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public class DependencyConstructionException : DependencyResolutionException
    {
        internal DependencyConstructionException() { }
        private DependencyConstructionException(string message, Exception inner) : base(message, inner) { }
        public DependencyConstructionException(Type type, Exception inner) 
            : this($"Failed to initialize dependency of type {type.VerifyArgument(nameof(type)).Value.FullName}. ", inner) { }
        public DependencyConstructionException(Type type, string argName) : this(type, argName, null, null) { }
        public DependencyConstructionException(Type type, string argName, Exception inner) : this(type, argName, null, inner) { }
        public DependencyConstructionException(Type type, string argName, string message, Exception inner) 
            : this(
                string.Format(
                    "Unable to initialize dependency of type {0}. Could not satisfy constructor argument '{1}'{2}",
                    type.VerifyArgument(nameof(type)).Value.FullName,
                    argName.VerifyArgument(nameof(argName)).IsNotNullOrEmpty().Value,
                    $". {message ?? string.Empty}"), 
                inner) { }

        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        protected DependencyConstructionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}