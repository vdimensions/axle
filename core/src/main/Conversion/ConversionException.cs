using System;

using Axle.Verification;


namespace Axle.Conversion
{
    /// <summary>
    /// An exception thrown whenever a conversion form object of one given type fails to convert to an instance of another type.
    /// </summary>
    /// <seealso cref="IConverter{TSource,TTarget}"/>
    /// <seealso cref="ITwoWayConverter{TSource,TTarget}"/>
    public partial class ConversionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/> class.
        /// </summary>
        public ConversionException() {}
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public ConversionException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a <c>null</c> reference (<c>Nothing</c> in Visual Basic) 
        /// if no inner exception is specified.
        /// </param>
        public ConversionException(string message, Exception inner) : base(message, inner) {}

        /// <summary>
        /// Initializes a new instance of <see cref="ConversionException"/> to represent the failure of converting the given types.
        /// </summary>
        /// <param name="sourceType">
        /// The type of the source object that failed to convert.
        /// </param>
        /// <param name="destinationType">
        /// The destination type of the failed conversion.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either the <see cref="sourceType"/> or <see cref="destinationType"/> is <c>null</c>.
        /// </exception>
        public ConversionException(Type sourceType, Type destinationType) : this(
            string.Format(
                "Cannot convert an instance of {0} to {1}.", 
                sourceType.VerifyArgument(nameof(sourceType)).IsNotNull().Value.FullName, 
                destinationType.VerifyArgument(nameof(destinationType)).IsNotNull().Value.FullName),
            null) {}
        /// <summary>
        /// Initializes a new instance of <see cref="ConversionException"/> to represent the failure of converting the given types,
        /// and a reference to the inner exception that is the cause of this exception..
        /// </summary>
        /// <param name="sourceType">
        /// The type of the source object that failed to convert.
        /// </param>
        /// <param name="destinationType">
        /// The destination type of the failed conversion.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a <c>null</c> reference (<c>Nothing</c> in Visual Basic) 
        /// if no inner exception is specified.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either the <see cref="sourceType"/> or <see cref="destinationType"/> is <c>null</c>.
        /// </exception>
        public ConversionException(Type sourceType, Type destinationType, Exception inner) : this(
            string.Format(
                "Cannot convert an instance of {0} to {1}.", 
                sourceType.VerifyArgument(nameof(sourceType)).IsNotNull().Value.FullName, 
                destinationType.VerifyArgument(nameof(destinationType)).IsNotNull().Value.FullName), 
            inner) {}
    }
}