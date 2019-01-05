namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the CSS border image slice definition.
    /// </summary>
    struct BorderImageSlice : ICssValue
    {
        #region Fields

        private readonly Length _bottom;
        private readonly Length _left;
        private readonly Length _right;
        private readonly Length _top;
        private readonly Boolean _filled;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS border image slice definition.
        /// </summary>
        /// <param name="top">The top length.</param>
        /// <param name="right">The right length.</param>
        /// <param name="bottom">The bottom length.</param>
        /// <param name="left">The left length.</param>
        /// <param name="filled">True if the filled flag is enabled, otherwise false.</param>
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
        /// Gets the bottom coordinate.
        /// </summary>
        public Length Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the left coordinate.
        /// </summary>
        public Length Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets the top coordinate.
        /// </summary>
        public Length Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the right coordinate.
        /// </summary>
        public Length Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets if the slice should be filled.
        /// </summary>
        public Boolean IsFilled
        {
            get { return _filled; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                if (!_top.Equals(Length.Auto))
                {
                    sb.Append(_top.CssText);

                    if (!_right.Equals(Length.Auto))
                    {
                        sb.Append(Symbols.Space);
                        sb.Append(_right.CssText);
                    }

                    if (!_bottom.Equals(Length.Auto))
                    {
                        sb.Append(Symbols.Space);
                        sb.Append(_bottom.CssText);
                    }

                    if (!_left.Equals(Length.Auto))
                    {
                        sb.Append(Symbols.Space);
                        sb.Append(_left.CssText);
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
        }

        #endregion
    }
}
