namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS image repeat definition.
    /// </summary>
    public sealed class CssImageRepeatsValue : ICssCompositeValue, IEquatable<CssImageRepeatsValue>
    {
        #region Fields

        private readonly ICssValue _horizontal;
        private readonly ICssValue _vertical;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS image repeat definition.
        /// </summary>
        /// <param name="horizontal">The horizontal part.</param>
        /// <param name="vertical">The vertical part.</param>
        public CssImageRepeatsValue(ICssValue horizontal, ICssValue vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the horizontal repeat component.
        /// </summary>
        public ICssValue Horizontal => _horizontal;

        /// <summary>
        /// Gets the value of the vertical repeat component.
        /// </summary>
        public ICssValue Vertical => _vertical;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var h = _horizontal.CssText;
                var v = _vertical.CssText;

                if (h.Isi(CssKeywords.Repeat) && v.Isi(CssKeywords.NoRepeat))
                {
                    return CssKeywords.RepeatX;
                }
                else if (v.Isi(CssKeywords.Repeat) && h.Isi(CssKeywords.NoRepeat))
                {
                    return CssKeywords.RepeatY;
                }
                else if (h == v)
                {
                    return h;
                }

                return String.Concat(h, " ", v);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssImageRepeatsValue other)
        {
            return _horizontal.Equals(other._horizontal) && _vertical.Equals(other._vertical);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssImageRepeatsValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var h = _horizontal.Compute(context);
            var v = _vertical.Compute(context);
            return new CssImageRepeatsValue(h, v);
        }

        #endregion
    }
}
