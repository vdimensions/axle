#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Text.Formatting
{
    /// <summary>
    /// A delegate representing a method used for formatting values.
    /// </summary>
    /// <param name="format">
    /// A formatting <see cref="string"/>.
    /// </param>
    /// <param name="arg">
    /// The object to be formatted.
    /// </param>
    /// <param name="formatProvider">
    /// A <see cref="IFormatProvider"/> instance that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A copy of the <paramref name="format">format</paramref> in which the format item or items have been replaced by
    /// the string representation of <paramref name="arg"/>.
    /// </returns>
    /// <see cref="string.Format(System.IFormatProvider,string,object[])"/>
    public delegate string FormatMethod(string format, object arg, IFormatProvider formatProvider);
    
    /// <summary>
    /// A delegate representing a method used for formatting values.
    /// </summary>
    /// <param name="format">
    /// A formatting <see cref="string"/>.
    /// </param>
    /// <param name="arg">
    /// The object, an instance of <typeparamref name="T"/>, to be formatted.
    /// </param>
    /// <param name="formatProvider">
    /// A <see cref="IFormatProvider"/> instance that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A copy of the <paramref name="format">format</paramref> in which the format item or items have been replaced by
    /// the string representation of <paramref name="arg"/>.
    /// </returns>
    /// <typeparam name="T">
    /// The type of the object to be formatted.
    /// </typeparam>
    /// <see cref="string.Format(System.IFormatProvider,string,object[])"/>
    public delegate string FormatMethod<in T>(string format, T arg, IFormatProvider formatProvider);
    
    /// <summary>
    /// A delegate representing a method used for formatting values.
    /// </summary>
    /// <param name="format">
    /// A formatting <see cref="string"/>.
    /// </param>
    /// <param name="arg">
    /// The object, an instance of <typeparamref name="T"/>, to be formatted.
    /// </param>
    /// <param name="formatProvider">
    /// A <typeparamref name="TFormatProvider"/> instance that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A copy of the <paramref name="format">format</paramref> in which the format item or items have been replaced by
    /// the string representation of <paramref name="arg"/>.
    /// </returns>
    /// <typeparam name="T">
    /// The type of the object to be formatted.
    /// </typeparam>
    /// <typeparam name="TFormatProvider">
    /// The type of the <see cref="IFormatProvider"/> instance used during formatting.
    /// </typeparam>
    /// <see cref="string.Format(System.IFormatProvider,string,object[])"/>
    public delegate string FormatMethod<in T, in TFormatProvider>(string format, T arg, TFormatProvider formatProvider);
}
#endif