namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    public struct ImageRepeats : ICssValue
    {
        private readonly ICssValue _horizontal;
        private readonly ICssValue _vertical;

        public ImageRepeats(ICssValue horizontal, ICssValue vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        public ICssValue Horizontal
        {
            get { return _horizontal; }
        }

        public ICssValue Vertical
        {
            get { return _vertical; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

        public override String ToString()
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
