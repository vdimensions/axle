#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Security;

namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse character array values into to a <see cref="SecureString"/> instance.
    /// <remarks>
    /// Unlike the other <see cref="IParser"/> implementations, this class explicitly throws a
    /// <see cref="SecurityException"/> when the <see cref="IParser.Parse(string,System.IFormatProvider)"/> and the
    /// <see cref="IParser.Parse(string)"/>  methods are called. In addition, the
    /// <see cref="IParser.TryParse(string,System.IFormatProvider,out object)"/>
    /// and <see cref="IParser.TryParse(string,out object)"/> methods reject the input by always returning <c>false</c>.
    /// <para>
    /// The justification for this behavior is that a <see cref="SecureString"/> instance should not be constructed from
    /// an already instantiated <see cref="string">managed string</see> object.
    /// Calling the listed above method overloads is a strong indication for misuse of the <see cref="SecureString"/>
    /// class, and a potential security vulnerability in the caller.
    /// </para>
    /// </remarks>
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class SecureStringParser : AbstractParser<SecureString>
    {
        /// <inheritdoc />
        public override bool Validate(char[] value, IFormatProvider formatProvider) => true;

        /// <inheritdoc />
        public override bool Validate(string value, IFormatProvider formatProvider)
        {
            // Do not allow managed string values to be parsed
            return false;
        }

        /// <inheritdoc />
        protected override SecureString DoParse(char[] value, IFormatProvider formatProvider)
        {
            var result = new SecureString();
            foreach (var c in value)
            {
                result.AppendChar(c);
            }
            result.MakeReadOnly();
            return result;
        }

        /// <inheritdoc />
        protected override SecureString DoParse(string value, IFormatProvider formatProvider)
        {
            throw new SecurityException(
                "Parsing a secured string from a managed string instance is not allowed -- it defeats the purpose of the SecureString class. ");
        }
    }
}
#endif