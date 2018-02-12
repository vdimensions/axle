using System;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.Text.Formatting
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    public class FormatProvider : IFormatProvider, ICustomFormatter
    {
        public static FormatProvider Create(ICustomFormatter formatter)
        {
            return new FormatProvider(formatter);
        }
        public static FormatProvider Create<TCF>() where TCF: ICustomFormatter, new()
        {
            return new FormatProvider(new TCF());
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ICustomFormatter customFormatter;

        public FormatProvider(ICustomFormatter formatter)
        {
            customFormatter = formatter.VerifyArgument(nameof(formatter)).IsNotNull().Value;
        }

        public object GetFormat(Type formatType) { return SupportFormatter(formatType) ? this : null; }

        protected virtual bool SupportFormatter(Type formatType)
        {
            return typeof(ICustomFormatter).Equals(formatType);
        }

        #region Implementation of ICustomFormatter
        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
        {
            return customFormatter.Format(format, arg, formatProvider);
        }
        #endregion
    }
}