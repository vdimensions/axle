using System;
using System.Globalization;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Runtime.Serialization;
#endif

using Axle.Verification;


namespace Axle.Resources
{
    /// <summary>
    /// Represents errors that occur while loading a resource object. 
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public sealed class ResourceLoadException : ResourceException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ResourceLoadException"/> class.
        /// </summary>
        public ResourceLoadException() { }

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceLoadException"/> class with a specified error <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public ResourceLoadException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceLoadException"/> class with a specified error <paramref name="message"/>
        /// and a reference to the <paramref name="inner"/> exception that is the cause of the current exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a <c>null</c> reference (<c>Nothing</c> in Visual Basic) 
        /// if no inner exception is specified.
        /// </param>
        public ResourceLoadException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceLoadException"/> class with a specified resource parameters, such as
        /// <paramref name="resourceName"/>, <paramref name="bundleName"/> and <paramref name="culture"/>.
        /// </summary>
        /// <param name="resourceName">
        /// The name of the resource that was looked up.
        /// </param>
        /// <param name="bundleName">
        /// The name of the bundle which was expected to contain the resource. 
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> that the resource was requested for. 
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. This parameter is mandatory.
        /// </param>
        public ResourceLoadException(string resourceName, string bundleName, CultureInfo culture, Exception inner) : this(
                string.Format("An error occurred while loading resource '{0}' from bundle '{1}' and culture '{2}' See the inner exception for more details.", resourceName, bundleName, culture.DisplayName),
                inner.VerifyArgument(nameof(inner)).IsNotNull()) { }

        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoadException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown. 
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination. 
        /// </param>
        internal ResourceLoadException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}