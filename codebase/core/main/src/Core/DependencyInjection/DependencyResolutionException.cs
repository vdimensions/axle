using System;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Runtime.Serialization;
#endif

using Axle.Verification;


namespace Axle.Core.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public class DependencyResolutionException : Exception
    {
        private readonly bool _isRecoverable = true;

        public DependencyResolutionException() { }
        public DependencyResolutionException(bool isRecoverable)
        {
            _isRecoverable = isRecoverable;
        }
        public DependencyResolutionException(string message) : base(message) { }
        public DependencyResolutionException(string message, bool isRecoverable) : base(message)
        {
            _isRecoverable = isRecoverable;
        }
        public DependencyResolutionException(string message, Exception inner) : base(message, inner) { }
        public DependencyResolutionException(string message, Exception inner, bool isRecoverable) : base(message, inner)
        {
            _isRecoverable = isRecoverable;
        }
        public DependencyResolutionException(Type type, string name, Exception inner) : this(type, name, null, inner) { }
        public DependencyResolutionException(Type type, string name, Exception inner, bool isRecoverable) : this(type, name, null, inner)
        {
            _isRecoverable = isRecoverable;
        }
        public DependencyResolutionException(Type type, string name, string message, Exception inner) : this(type, name, message, inner, true) { }
        public DependencyResolutionException(Type type, string name, string message, Exception inner, bool isRecoverable) : this(
            string.Format(
                "Unable to resolve dependency {0}of type {1}{2}",
                string.IsNullOrEmpty(name) ? string.Empty : $"'{name}' ",
                type.VerifyArgument(nameof(type)).Value.FullName,
                message == null ? string.Empty : $". {message}"), 
            inner)
        {
            _isRecoverable = isRecoverable;
        }
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        /// <inheritdoc />
        protected DependencyResolutionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif

        /// <summary>
        /// Gets a boolean value determining whether the current <see cref="DependencyResolutionException"/> is recoverable.
        /// </summary>
        public bool IsRecoverable => _isRecoverable;
    }
}