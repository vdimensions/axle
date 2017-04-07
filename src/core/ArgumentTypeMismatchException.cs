using System;
using System.Runtime.Serialization;


namespace Axle
{
    /// <summary>
    /// The exception that is thrown if one of the arguments of a method is not of the expected type.
    /// </summary>
    /// <seealso cref="ArgumentException" />
    [Serializable]
    public class ArgumentTypeMismatchException : ArgumentException
    {
        internal static string FormatMessage(Type expectedType, Type actualType)
        {
            return string.Format(
                "Argument was not of the correct type. Expecting {0}, but found {1}",
                expectedType.FullName,
                actualType.FullName);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ArgumentTypeMismatchException" /> class.
        /// </summary>
        public ArgumentTypeMismatchException() {}
        /// <summary>
        /// Initializes a new instance of <see cref="ArgumentTypeMismatchException" /> class with a specified error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        public ArgumentTypeMismatchException(string message) : base(message) {}
        /// <summary>
        /// Initializes a new instance of <see cref="ArgumentTypeMismatchException" /> class with a specified error message and
        /// a reference to the inner exception that is the cause of this exception. 
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. 
        /// If the innerException parameter is not a null reference, the current exception is raised in a catch block 
        /// that handles the inner exception. 
        /// </param>
        public ArgumentTypeMismatchException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException" /> class with a specified error 
        /// message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="paramName">The name of the parameter that caused the current exception. </param>
        public ArgumentTypeMismatchException(string message, string paramName) : base(message, paramName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException" /> class with a specified error 
        /// message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="paramName">The name of the parameter that caused the current exception. </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException parameter is not a null reference, 
        /// the current exception is raised in a catch block that handles the inner exception. 
        /// </param>
        public ArgumentTypeMismatchException(string message, string paramName, Exception inner) : base(message, paramName, inner) { }
        public ArgumentTypeMismatchException(Type expectedType, Type actualType, string paramName)
            : this(FormatMessage(expectedType, actualType), paramName) { }
        public ArgumentTypeMismatchException(Type expectedType, Type actualType, string paramName, Exception inner)
            : this(FormatMessage(expectedType, actualType), paramName, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException" /> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data. </param>
        /// <param name="context">The contextual information about the source or destination. </param>
        protected ArgumentTypeMismatchException( SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// The exception that is thrown if one of the arguments of a method is not of the expected type.
    /// </summary>
    /// <typeparam name="TExpected">The expected type of the argument.</typeparam>
    [Serializable]
    public class ArgumentTypeMismatchException<TExpected> : ArgumentTypeMismatchException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ArgumentTypeMismatchException{TExpected}" /> class.
        /// </summary>
        public ArgumentTypeMismatchException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException" /> class with a specified error 
        /// message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception. </param>
        /// <param name="actualType">The actual type of the argument that caused the current exception. </param>
        public ArgumentTypeMismatchException(string paramName, Type actualType) : base(typeof(TExpected), actualType, paramName) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException" /> class with a specified error 
        /// message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception. </param>
        /// <param name="actualType">The actual type of the argument that caused the current exception. </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException parameter is not a null reference, 
        /// the current exception is raised in a catch block that handles the inner exception. 
        /// </param>
        public ArgumentTypeMismatchException(string paramName, Type actualType, Exception inner) : base(typeof(TExpected), actualType, paramName, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException{TExpected}" /> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data. </param>
        /// <param name="context">The contextual information about the source or destination. </param>
        protected ArgumentTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// The exception that is thrown if one of the arguments of a method is not of the expected type.
    /// </summary>
    /// <typeparam name="TExpected">The expected type of the argument.</typeparam>
    /// <typeparam name="T">The actual type of the argument.</typeparam>
    [Serializable]
    public class ArgumentTypeMismatchException<TExpected, T> : ArgumentTypeMismatchException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ArgumentTypeMismatchException{TExpected,T}" /> class.
        /// </summary>
        public ArgumentTypeMismatchException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException{Texpected,T}" /> class with a specified error 
        /// message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception. </param>
        public ArgumentTypeMismatchException(string paramName) : base(typeof(TExpected), typeof(T), paramName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException{Texpected,T}" /> class with a specified error 
        /// message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception. </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException parameter is not a null reference, 
        /// the current exception is raised in a catch block that handles the inner exception. 
        /// </param>
        public ArgumentTypeMismatchException(string paramName, Exception inner) : base(typeof(TExpected), typeof(T), paramName, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTypeMismatchException{TExpected,T}" /> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data. </param>
        /// <param name="context">The contextual information about the source or destination. </param>
        protected ArgumentTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}