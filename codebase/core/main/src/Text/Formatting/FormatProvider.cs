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

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatProvider"/> class with the provided
        /// <paramref name="formatter"/>
        /// </summary>
        /// <param name="formatter">
        /// An <see cref="ICustomFormatter"/> instance to delegate formatting to.
        /// </param>
        public FormatProvider(ICustomFormatter formatter)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(formatter, nameof(formatter)));
            _customFormatter = formatter;
        }

        /// <inheritdoc />
        public object GetFormat(Type formatType) => SupportFormatter(formatType) ? this : null;

        protected virtual bool SupportFormatter(Type formatType)
        {
            return typeof(ICustomFormatter) == formatType;
        }

        /// <inheritdoc />
        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
        {
            return _customFormatter.Format(format, arg, formatProvider);
        }
    }
}
#endif