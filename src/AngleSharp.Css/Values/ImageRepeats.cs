namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS image repeat definition.
    /// </summary>
    struct ImageRepeats : ICssValue
    {
        private readonly ICssValue _horizontal;
        private readonly ICssValue _vertical;

        /// <summary>
        /// Creates a new CSS image repeat definition.
        /// </summary>
        /// <param name="horizontal">The horizontal part.</param>
        /// <param name="vertical">The vertical part.</param>
        public ImageRepeats(ICssValue horizontal, ICssValue vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        /// <summary>
        /// Gets the value of the horizontal repeat component.
        /// </summary>
        public ICssValue Horizontal
        {
            get { return _horizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical repeat component.
        /// </summary>
        public ICssValue Vertical
        {
            get { return _vertical; }
        }

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
    }
}
