﻿using System;
using System.Globalization;


namespace Axle.Conversion.Parsing
{
    partial class TimeSpanParser : AbstractStrictParser<TimeSpan>
    {
        protected override TimeSpan DoParseExact(string value, string format, IFormatProvider formatProvider)
        {
            return TimeSpan.ParseExact(value, format, formatProvider, TimeSpanStyles.None);
        }

        public override bool TryParseExact(string value, string format, IFormatProvider formatProvider, out TimeSpan output)
        {
            return TimeSpan.TryParseExact(value, format, formatProvider, TimeSpanStyles.None, out output);
        }
    }
}