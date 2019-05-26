#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.Text.Formatting
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class FormatProvider : IFormatProvider, ICustomFormatter
    {
        public static FormatProvider Create(ICustomFormatter formatter)
        {
            return new FormatProvider(formatter);
        }
        public static FormatProvider Create<TF>() where TF: ICustomFormatter, new()
        {
            return new FormatProvider(new TF());
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ICustomFormatter _customFormatter;

        public FormatProvider(ICustomFormatter formatter)
        {
            _customFormatter = Verifier.IsNotNull(Verifier.VerifyArgument(formatter, nameof(formatter))).Value;
        }

        /// <inheritdoc />
        public object GetFormat(Type formatType) => SupportFormatter(formatType) ? this : null;

        protected virtual bool SupportFormatter(Type formatType)
        {
            return typeof(ICustomFormatter) == formatType;
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
#endif