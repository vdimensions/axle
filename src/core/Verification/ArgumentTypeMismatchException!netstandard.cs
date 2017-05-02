using System;
using System.Runtime.Serialization;


namespace Axle.Verification
{
    [Serializable]
    partial class ArgumentTypeMismatchException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException" /> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data. 
        /// </param>
        /// <param name="context">
        /// The contextual information about the source or destination. 
        /// </param>
        protected ArgumentTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    partial class ArgumentTypeMismatchException<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException{TExpected}" /> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data. 
        /// </param>
        /// <param name="context">
        /// The contextual information about the source or destination. 
        /// </param>
        protected ArgumentTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    partial class ArgumentTypeMismatchException<TExpected, T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException{TExpected,T}" /> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data. 
        /// </param>
        /// <param name="context">
        /// The contextual information about the source or destination. 
        /// </param>
        protected ArgumentTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}