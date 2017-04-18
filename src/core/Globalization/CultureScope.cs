using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;


namespace Axle.Globalization
{
    /// <summary>
    /// Represents a disposable scope, which allows changing the current culture/ui culture of the executing thread to the 
    /// respective <see cref="CultureScope.Culture"/> and <see cref="CultureScope.UICulture"/> troughout its lifetine. 
    /// Upon disposing, the culture settings prior initializig the scope are rolled back.
    /// </summary>
    public sealed class CultureScope : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="CultureScope"/> instance using the specified <paramref name="culture"/>.
        /// </summary>
        /// <param name="culture">
        /// The <see cref="CultureInfo">culture</see> to be set as default culture and UI culture by the newly created <see cref="CultureScope">culture scope</see>.
        /// </param>
        /// <returns>
        /// A new <see cref="CultureScope"/> instance.
        /// </returns>
        public static CultureScope Create(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }
            return new CultureScope(culture, culture);
        }
        /// <summary>
        /// Creates a new <see cref="CultureScope"/> instance using the specified <paramref name="culture"/> and <paramref name="uiCulture">UI culture</paramref>.
        /// </summary>
        /// <param name="culture">
        /// The <see cref="CultureInfo">culture</see> to be set as default culture by the newly created <see cref="CultureScope">culture scope</see>.
        /// </param>
        /// <param name="uiCulture">
        /// The <see cref="CultureInfo">culture</see> to be set as default UI culture by the newly created <see cref="CultureScope">culture scope</see>.
        /// </param>
        /// <returns>
        /// A new <see cref="CultureScope"/> instance.
        /// </returns>
        public static CultureScope Create(CultureInfo culture, CultureInfo uiCulture)
        {
            if (culture == null && uiCulture == null)
            {
                throw new ArgumentException("At least one of the 'culture' or 'uiCulture' arguments must be non-null.");
            }
            return new CultureScope(culture, uiCulture);
        }
        /// <summary>
        /// Creates a new <see cref="CultureScope"/> instance using the specified <paramref name="culture"/>.
        /// </summary>
        /// <param name="culture">
        /// The name for the <see cref="CultureInfo">culture</see> to be set as default culture and UI culture by the newly created <see cref="CultureScope">culture scope</see>.
        /// </param>
        /// <returns>
        /// A new <see cref="CultureScope"/> instance.
        /// </returns>
        public static CultureScope Create(string culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }
            return new CultureScope(culture, culture);
        }
        /// <summary>
        /// Creates a new <see cref="CultureScope"/> instance using the specified <paramref name="culture"/> and <paramref name="uiCulture">UI culture</paramref>.
        /// </summary>
        /// <param name="culture">
        /// The name of the <see cref="CultureInfo">culture</see> to be set as default culture by the newly created <see cref="CultureScope">culture scope</see>.
        /// </param>
        /// <param name="uiCulture">
        /// The name of the <see cref="CultureInfo">culture</see> to be set as default UI culture by the newly created <see cref="CultureScope">culture scope</see>.
        /// </param>
        /// <returns>
        /// A new <see cref="CultureScope"/> instance.
        /// </returns>
        public static CultureScope Create(string culture, string uiCulture)
        {
            if (culture == null && uiCulture == null)
            {
                throw new ArgumentException("At least one of the 'culture' or 'uiCulture' arguments must be non-null.");
            }
            return new CultureScope(culture, uiCulture);
        }

        /// <summary>
        /// Creates a new <see cref="CultureScope"/> instance using an invariant <see cref="CultureInfo">culture</see>.
        /// </summary>
        /// <returns>
        /// A new <see cref="CultureScope"/> instance.
        /// </returns>
        public static CultureScope CreateInvariant()
        {
            return Create(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the <see cref="CultureInfo">culture</see> for the current <see cref="CultureScope">culture scope</see>.
        /// </summary>
        public static CultureInfo CurrentCulture { get { return Thread.CurrentThread.CurrentCulture; } }
        /// <summary>
        /// Gets the <see cref="CultureInfo">culture</see> for the current <see cref="CultureScope">culture scope</see>.
        /// </summary>
        public static CultureInfo CurrentUICulture { get { return Thread.CurrentThread.CurrentUICulture; } }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CultureInfo previousCulture;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CultureInfo previousUICulture;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CultureInfo currentCulture;
        /// <summary>
        /// Gets the <see cref="CultureInfo">culture</see> that the current <see cref="CultureScope">culture scope</see> was initialized with.
        /// </summary>
        public CultureInfo Culture { get { return currentCulture; } }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CultureInfo currentUICulture;
        /// <summary>
        /// Gets the <see cref="CultureInfo">UI culture</see> that the current <see cref="CultureScope">culture scope</see> was initialized with.
        /// </summary>
        public CultureInfo UICulture { get { return currentUICulture; } }

        private CultureScope()
        {
            var thread = Thread.CurrentThread;
            this.previousCulture = thread.CurrentCulture;
            this.previousUICulture = thread.CurrentUICulture;
        }
        private CultureScope(CultureInfo culture, CultureInfo uiCulture) : this()
        {
            var thread = Thread.CurrentThread;
            this.currentCulture = culture;
            if (culture != null)
            {
                thread.CurrentCulture = culture;
            }
            this.currentUICulture = uiCulture;
            if (uiCulture != null)
            {
                thread.CurrentUICulture = uiCulture;
            }
        }
        private CultureScope(string culture, string uiCulture) : this(
            culture == null ? null : CultureInfo.CreateSpecificCulture(culture),
            uiCulture == null ? null : CultureInfo.CreateSpecificCulture(uiCulture)) { }

        #region Implementation of IDisposable
        /// <summary>
        /// Disposes the current <see cref="CultureScope"/> instance and attempts to 
        /// reset the <see cref="Thread.CurrentThread.CurrentCulture"/> and the
        /// <see cref="Thread.CurrentThread.CurrentUICulture"/> properties to their
        /// values prior creating the scope.
        /// </summary>
        public void Dispose()
        {
            var thread = Thread.CurrentThread;
            if (this.currentCulture != null && this.previousCulture != null && thread.CurrentCulture == this.currentCulture)
            {
                thread.CurrentCulture = this.previousCulture;
            }
            if (this.currentUICulture != null && this.previousUICulture != null && thread.CurrentUICulture == this.currentUICulture)
            {
                thread.CurrentUICulture = this.previousUICulture;
            }
        }
        void IDisposable.Dispose() 
        { 
            this.Dispose(); 
        }
        #endregion
    }
}
