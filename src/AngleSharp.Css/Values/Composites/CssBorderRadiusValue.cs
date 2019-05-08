namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents a border radius value.
    /// </summary>
    sealed class CssBorderRadiusValue : ICssCompositeValue
    {
        #region Fields

        private readonly CssPeriodicValue _horizontal;
        private readonly CssPeriodicValue _vertical;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new border radius value.
        /// </summary>
        public CssBorderRadiusValue(CssPeriodicValue horizontal, CssPeriodicValue vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal values.
        /// </summary>
        public CssPeriodicValue Horizontal => _horizontal;

        /// <summary>
        /// Gets the vertical values.
        /// </summary>
        public CssPeriodicValue Vertical => _vertical;

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
