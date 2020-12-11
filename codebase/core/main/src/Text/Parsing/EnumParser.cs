﻿using System;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A generic <see cref="IParser{T}"/> implementation that can handle <see cref="Enum">enum</see> types.
    /// </summary>
    /// <typeparam name="T">
    /// An enumeration type.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumParser<T> : AbstractParser<T> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <inheritdoc />
        protected override T DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            try
            {
                // TODO: Create enum format provider specifying case option
                return (T) Enum.Parse(typeof(T), value.ToString());
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                if (value.ToString().Trim().StartsWith("-", StringComparison.Ordinal))
                {
                    var longParser = new Int64Parser();
                    if (longParser.TryParse(value, formatProvider, out var res))
                    {
                        return (T) Enum.ToObject(typeof(T), res);
                    }
                } 
                else
                {
                    var ulongParser = new UInt64Parser();
                    if (ulongParser.TryParse(value, formatProvider, out var res))
                    {
                        return (T) Enum.ToObject(typeof(T), res);
                    }
                }
                throw;
            }
        }
    }
}