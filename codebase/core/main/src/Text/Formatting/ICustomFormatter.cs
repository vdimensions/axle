#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Text.Formatting
{
    /// <summary>
    /// A generic interface derived from the <see cref="IFormatProvider"/> that is specific about the formatted object 
    /// type.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the formatted object.
    /// </typeparam>
    /// <seealso cref="ICustomFormatter"/>
    /// <seealso cref="IFormatProvider"/>
    public interface ICustomFormatter<T> : ICustomFormatter
    {
        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation 
        /// using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">
        /// A format string containing formatting specifications. 
        /// </param>
        /// <param name="arg">
        /// The <typeparamref name="T"/> instance to format. 
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> instance that supplies format information about the current instance. 
        /// </param>
        /// <returns>
        /// The string representation of the value of <paramref name="arg"/>, formatted as specified by 
        /// <paramref name="format"/> and <paramref name="formatProvider"/>.
        /// </returns>
        string Format(string format, T arg, IFormatProvider formatProvider);
    }

    /// <summary>
    /// A generic interface derived from the <see cref="IFormatProvider"/> that is specific about the formatted object 
    /// type and the <seealso cref="IFormatProvider"/> type that is used for formatting.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the formatted object.
    /// </typeparam>
    /// <typeparam name="TFP">
    /// A type that implements the <see cref="IFormatProvider"/> interface.
    /// </typeparam>
    /// <seealso cref="ICustomFormatter{T}"/>
    /// <seealso cref="ICustomFormatter"/>
    /// <seealso cref="IFormatProvider"/>
    public interface ICustomFormatter<T, TFP> : ICustomFormatter<T> where TFP: IFormatProvider
    {
        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation 
        /// using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">
        /// A format string containing formatting specifications. 
        /// </param>
        /// <param name="arg">
        /// The <typeparamref name="T"/> instance to format. 
        /// </param>
        /// <param name="formatProvider">
        /// An <typeparamref name="TFP"/> instance that supplies format information about the current instance. 
        /// </param>
        /// <returns>
        /// The string representation of the value of <paramref name="arg"/>, formatted as specified by 
        /// <paramref name="format"/> and <paramref name="formatProvider"/>.
        /// </returns>
        string Format(string format, T arg, TFP formatProvider);
    }
}
#endif