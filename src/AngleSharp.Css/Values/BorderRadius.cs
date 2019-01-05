namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a border radius value.
    /// </summary>
    class BorderRadius : ICssValue
    {
        #region Fields

        private readonly Periodic<ICssValue> _horizontal;
        private readonly Periodic<ICssValue> _vertical;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new border radius value.
        /// </summary>
        public BorderRadius(Periodic<ICssValue> horizontal, Periodic<ICssValue> vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal values.
        /// </summary>
        public Periodic<ICssValue> Horizontal => _horizontal;

        /// <summary>
        /// Gets the vertical values.
        /// </summary>
        public Periodic<ICssValue> Vertical => _vertical;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var h = _horizontal.CssText;

                if (!Object.ReferenceEquals(_horizontal, _vertical))
                {
                    var v = _vertical.CssText;

                    if (!String.IsNullOrEmpty(v) && h != v)
                    {
                        return String.Concat(h, " / ", v);
                    }
                }

                return h;
            }
        }

        #endregion
    }
}
