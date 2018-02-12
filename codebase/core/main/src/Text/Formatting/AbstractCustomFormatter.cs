using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Axle.Text.Formatting
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    public abstract class AbstractCustomFormatter<T> : ICustomFormatter<T>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly IList<string> customFormats;

        protected AbstractCustomFormatter() : this(null) { }
        protected AbstractCustomFormatter(params string[] customFormats) : this(customFormats as IEnumerable<string>) { }
        protected AbstractCustomFormatter(IEnumerable<string> customFormats)
        {
            if (customFormats != null)
            {
                this.customFormats = customFormats.OrderByDescending(x => x.Length).ToArray();
            }
        }

        internal bool TryFormatChunks(string format, T arg, IFormatProvider formatProvider, out string result)
        {
            if (string.IsNullOrEmpty(format) || customFormats == null || customFormats.Count == 0)
            {
                result = null;
                return false;
            }

            var sb = new StringBuilder(format);
            foreach (var f in customFormats)
            {
                ReplaceFormat(sb, f, arg, formatProvider);
            }
            var res = sb.ToString();
            if (res.Equals(format, StringComparison.Ordinal))
            {
                result = null;
            }
            else
            {
                result = res;
                return true;
            }
            return false;
        }

        protected virtual void ReplaceFormat(StringBuilder sb, string format, T arg, IFormatProvider formatProvider)
        {
            ReplaceFormatInternal(sb, format, arg, formatProvider);
        }
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal void ReplaceFormatInternal(StringBuilder sb, string format, T arg, IFormatProvider formatProvider)
        {
            sb.Replace(format, DoFormat(format, arg, formatProvider));
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var typedArg = (T) arg;
            return Format(format, typedArg, formatProvider);
        }
        public string Format(string format, T arg, IFormatProvider formatProvider)
        {
            string result;
            if (!TryFormatChunks(format, arg, formatProvider, out result))
            {
                result = DoFormat(format, arg, formatProvider);
            }
            return result;
        }
        protected abstract string DoFormat(string format, T arg, IFormatProvider formatProvider);
    }

    #if !NETSTANDARD
    [Serializable]
    #endif
    public abstract class AbstractCustomFormatter<T, TFP> : AbstractCustomFormatter<T>, ICustomFormatter<T, TFP> 
        where TFP: class, IFormatProvider
    {
        protected AbstractCustomFormatter() : this(null) { }
        protected AbstractCustomFormatter(params string[] customFormats) : base(customFormats) { }
        protected AbstractCustomFormatter(IEnumerable<string> customFormats) : base(customFormats) { }

        public string Format(string formatString, T arg, TFP formatProvider)
        {
            string result;
            if (!TryFormatChunks(formatString, arg, formatProvider, out result))
            {
                result = DoFormat(formatString, arg, formatProvider);
            }
            return result;
        }

        protected sealed override string DoFormat(string format, T arg, IFormatProvider formatProvider)
        {
            return DoFormat(format, arg, (TFP) formatProvider);
        }
        protected abstract string DoFormat(string format, T arg, TFP formatProvider);

        protected sealed override void ReplaceFormat(StringBuilder sb, string format, T arg, IFormatProvider formatProvider)
        {
            ReplaceFormat(sb, format, arg, (TFP) formatProvider);
        }
        protected virtual void ReplaceFormat(StringBuilder sb, string format, T arg, TFP formatProvider)
        {
            sb.Replace(format, DoFormat(format, arg, formatProvider));
        }
    }
}
