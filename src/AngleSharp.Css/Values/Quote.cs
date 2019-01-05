namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS quote.
    /// </summary>
    struct Quote : ICssValue
    {
        #region Fields

        private readonly String _open;
        private readonly String _close;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS quote.
        /// </summary>
        /// <param name="open">The open quote character(s).</param>
        /// <param name="close">The close quote character(s).</param>
        public Quote(String open, String close)
        {
            _open = open;
            _close = close;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the open quote character(s).
        /// </summary>
        public String Open => _open;

        /// <summary>
        /// Gets the close quote character(s).
        /// </summary>
        public String Close => _close;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_open.CssString(), " ", _close.CssString());

        #endregion
    }
}
