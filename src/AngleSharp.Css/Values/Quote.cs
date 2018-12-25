namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS quote.
    /// </summary>
    public struct Quote : ICssValue
    {
        private readonly String _open;
        private readonly String _close;

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

        /// <summary>
        /// Gets the open quote character(s).
        /// </summary>
        public String Open
        {
            get { return _open; }
        }

        /// <summary>
        /// Gets the close quote character(s).
        /// </summary>
        public String Close
        {
            get { return _close; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return String.Concat(_open.CssString(), " ", _close.CssString()); }
        }
    }
}
