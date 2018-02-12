using System;
using System.Runtime.Serialization;


namespace Axle.Resources
{
    [Serializable]
    partial class ResourceException
    {
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
    }
}