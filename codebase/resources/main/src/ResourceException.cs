using System;
#if !NETSTANDARD
using System.Runtime.Serialization;
#endif


namespace Axle.Resources
{
    /// <summary>
    /// Represents errors that occur while working with resources.
    /// </summary>
    #if !NETSTANDARD
    [Serializable]
    #endif
    public class ResourceException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ResourceException"/> class.
        /// </summary>
        public ResourceException() { }
        /// <summary>
        /// Creates a new instance of the <see cref="ResourceException"/> class with a specified error <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public ResourceException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ResourceException"/> class with a specified error <paramref name="message"/>
        /// and a reference to the <paramref name="inner"/> exception that is the cause of the current exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a <c>null</c> reference (<c>Nothing</c> in Visual Basic) 
        /// if no inner exception is specified.
        /// </param>
        public ResourceException(string message, Exception inner) : base(message, inner) { }
        #if !NETSTANDARD
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown. 
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination. 
        /// </param>
        protected ResourceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}