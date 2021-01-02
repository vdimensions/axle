using System;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System.Runtime.Serialization;
#endif

namespace Axle.Text.Parsing
{
    /// <summary>
    /// An exception that is thrown when failing to parse a <see cref="string"/> expression into a meaningful value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public class ParseException : FormatException
    {
        private const string MessageFormat = "Unable to parse an instance of {0} from the string '{1}'.";
        private const string MessageFormatExact = MessageFormat + " The expected value format is '{2}'.";

        /// <summary>
        /// Creates a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        public ParseException() { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParseException"/> class with a specified error 
        /// <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public ParseException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParseException"/> class with a specified error 
        /// <paramref name="message"/> and a reference to the <paramref name="inner"/> exception that is the cause of 
        /// this exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="inner">
        /// The <see cref="Exception">exception</see> that is the cause of the current exception.
        /// If the <paramref name="inner"/> parameter is not a <c><see langword="null"/></c> reference 
        /// (<c>Nothing</c> in Visual Basic), the current exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
        public ParseException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParseException"/> class with information about the 
        /// <see cref="string"/> <paramref name="value"/>being parsed and the target <paramref name="type"/> of the 
        /// parsing.
        /// </summary>
        /// <param name="value">
        /// The input <see cref="string"/> that was parsed.
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/> representing the desired result type of the parsed value.
        /// </param>
        public ParseException(string value, Type type) : this(string.Format(MessageFormat, type.FullName, value)) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParseException"/> class with information about the 
        /// <see cref="string"/> <paramref name="value"/>being parsed and the target <paramref name="type"/> of the 
        /// parsing.
        /// </summary>
        /// <param name="value">
        /// The input <see cref="string"/> that was parsed.
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/> representing the desired result type of the parsed value.
        /// </param>
        /// <param name="inner">
        /// The <see cref="Exception">exception</see> that is the cause of the current exception.
        /// If the <paramref name="inner"/> parameter is not a <c><see langword="null"/></c> reference 
        /// (<c>Nothing</c> in Visual Basic), the current exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
        public ParseException(string value, Type type, Exception inner) 
            : this(string.Format(MessageFormat, type.FullName, value), inner) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParseException"/> class with information about the 
        /// <see cref="string"/> <paramref name="value"/>being parsed, the <paramref name="format"/> used to represent
        /// the value, and the target <paramref name="type"/> of the parsing.
        /// </summary>
        /// <param name="value">
        /// The input <see cref="string"/> that was parsed.
        /// </param>
        /// <param name="format">
        /// A <see cref="string"/> that represents the format that is used to represent the value.
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/> representing the desired result type of the parsed value.
        /// </param>
        public ParseException(string value, string format, Type type) 
            : this(string.Format(MessageFormatExact, type.FullName, value, format)) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParseException"/> class with information about the 
        /// <see cref="string"/> <paramref name="value"/>being parsed, the <paramref name="format"/> used to represent
        /// the value, and the target <paramref name="type"/> of the parsing.
        /// </summary>
        /// <param name="value">
        /// The input <see cref="string"/> that was parsed.
        /// </param>
        /// <param name="format">
        /// A <see cref="string"/> that represents the format that is used to represent the value.
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/> representing the desired result type of the parsed value.
        /// </param>
        /// <param name="inner">
        /// The <see cref="Exception">exception</see> that is the cause of the current exception.
        /// If the <paramref name="inner"/> parameter is not a <c><see langword="null"/></c> reference 
        /// (<c>Nothing</c> in Visual Basic), the current exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
        public ParseException(string value, string format, Type type, Exception inner) 
            : this(string.Format(MessageFormatExact, type.FullName, value, format), inner) { }
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Creates a new instance of the <see cref="ParseException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data.
        /// </param>
        /// <param name="context">
        /// The contextual information about the source or destination.
        /// </param>
        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}
