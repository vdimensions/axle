using System;
using System.Globalization;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Runtime.Serialization;
#endif


namespace Axle.Resources
{
    /// <summary>
    /// Represents an error that occurs if a certain resource cannot be found.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class ResourceNotFoundException : ResourceException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ResourceNotFoundException"/> class.
        /// </summary>
        public ResourceNotFoundException() { }

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceNotFoundException"/> class with a specified error <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public ResourceNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceNotFoundException"/> class with a specified error <paramref name="message"/>
        /// and a reference to the <paramref name="inner"/> exception that is the cause of the current exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a <c>null</c> reference (<c>Nothing</c> in Visual Basic) 
        /// if no inner exception is specified.
        /// </param>
        public ResourceNotFoundException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceNotFoundException"/> class with a specified resource parameters, such as
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
        public ResourceNotFoundException(string resourceName, string bundleName, CultureInfo culture) : this(resourceName, bundleName, culture, null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceNotFoundException"/> class with a specified resource parameters, such as
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
        /// The exception that is the cause of the current exception, or a <c>null</c> reference (<c>Nothing</c> in Visual Basic) 
        /// if no inner exception is specified.
        /// </param>
        public ResourceNotFoundException(string resourceName, string bundleName, CultureInfo culture, Exception inner) : this(
                string.Format("Unable to locate resource '{0}' in bundle '{1}' for culture '{2}'.",  resourceName, bundleName, culture.DisplayName), 
                inner) { }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown. 
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination. 
        /// </param>
        internal ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}