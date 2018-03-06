using System;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.Text.Formatting
{
    #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
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
        private readonly ICustomFormatter _customFormatter;

        public FormatProvider(ICustomFormatter formatter)
        {
            _customFormatter = formatter.VerifyArgument(nameof(formatter)).IsNotNull().Value;
        }

        /// <inheritdoc />
        public object GetFormat(Type formatType) => SupportFormatter(formatType) ? this : null;

        protected virtual bool SupportFormatter(Type formatType)
        {
            return typeof(ICustomFormatter).Equals(formatType);
        }

        #region Implementation of ICustomFormatter
        /// <inheritdoc />
        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
        {
            return _customFormatter.Format(format, arg, formatProvider);
        }
        #endregion
    }
}