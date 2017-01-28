namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    public struct BorderImageSlice : ICssValue
    {
        #region Fields

        private readonly Length _bottom;
        private readonly Length _left;
        private readonly Length _right;
        private readonly Length _top;
        private readonly Boolean _filled;

        #endregion

        #region ctor

        public BorderImageSlice(Length top, Length right, Length bottom, Length left, Boolean filled)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
            _filled = filled;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

        #endregion

        #region Methods

        public override String ToString()
        {
            var sb = StringBuilderPool.Obtain();

            if (!_top.Equals(Length.Auto))
            {
                sb.Append(_top.ToString());

                if (!_right.Equals(Length.Auto))
                {
                    sb.Append(Symbols.Space);
                    sb.Append(_right.ToString());
                }

                if (!_bottom.Equals(Length.Auto))
                {
                    sb.Append(Symbols.Space);
                    sb.Append(_bottom.ToString());
                }

                if (!_left.Equals(Length.Auto))
                {
                    sb.Append(Symbols.Space);
                    sb.Append(_left.ToString());
                }

                if (_filled)
                {
                    sb.Append(Symbols.Space);
                }
            }

            if (_filled)
            {
                sb.Append(CssKeywords.Fill);
            }

            return sb.ToPool();
        }

        #endregion
    }
}
