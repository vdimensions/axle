﻿using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a <see cref="double">double precision floating point number</see> to a valid <see cref="double"/> value.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    //[Stateless]
    /// <summary>
    /// A parser class for <see cref="Double"/>
    /// </summary>
    internal sealed class DoubleParser : AbstractParser<double>
    {
        protected override Double DoParse(string value, IFormatProvider formatProvider)
        {
            return Double.Parse(value, NumberStyles.Any, formatProvider);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out Double output)
        {
            return Double.TryParse(value, NumberStyles.Any, formatProvider, out output);
        }
    }
}