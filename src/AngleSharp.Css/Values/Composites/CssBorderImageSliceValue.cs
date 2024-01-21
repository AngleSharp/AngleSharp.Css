namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the CSS border image slice definition.
    /// </summary>
    /// <remarks>
    /// Creates a new CSS border image slice definition.
    /// </remarks>
    /// <param name="top">The top length.</param>
    /// <param name="right">The right length.</param>
    /// <param name="bottom">The bottom length.</param>
    /// <param name="left">The left length.</param>
    /// <param name="filled">True if the filled flag is enabled, otherwise false.</param>
    public sealed class CssBorderImageSliceValue(ICssValue top, ICssValue right, ICssValue bottom, ICssValue left, Boolean filled) : ICssCompositeValue, IEquatable<CssBorderImageSliceValue>
    {
        #region Fields

        private readonly ICssValue _bottom = bottom;
        private readonly ICssValue _left = left;
        private readonly ICssValue _right = right;
        private readonly ICssValue _top = top;
        private readonly Boolean _filled = filled;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bottom coordinate.
        /// </summary>
        public ICssValue Bottom => _bottom;

        /// <summary>
        /// Gets the left coordinate.
        /// </summary>
        public ICssValue Left => _left;

        /// <summary>
        /// Gets the top coordinate.
        /// </summary>
        public ICssValue Top => _top;

        /// <summary>
        /// Gets the right coordinate.
        /// </summary>
        public ICssValue Right => _right;

        /// <summary>
        /// Gets if the slice should be filled.
        /// </summary>
        public Boolean IsFilled => _filled;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                if (!_top.Equals(CssLengthValue.Auto))
                {
                    sb.Append(_top.CssText);

                    if (!_right.Equals(CssLengthValue.Auto))
                    {
                        sb.Append(Symbols.Space);
                        sb.Append(_right.CssText);
                    }

                    if (!_bottom.Equals(CssLengthValue.Auto))
                    {
                        sb.Append(Symbols.Space);
                        sb.Append(_bottom.CssText);
                    }

                    if (!_left.Equals(CssLengthValue.Auto))
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

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssBorderImageSliceValue other)
        {
            return _filled == other._filled && _bottom.Equals(other._bottom) && _left.Equals(other._left) && _right.Equals(other._right) && _top.Equals(other._top);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssBorderImageSliceValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var bottom = (CssLengthValue)((ICssValue)_bottom).Compute(context);
            var left = (CssLengthValue)((ICssValue)_left).Compute(context);
            var right = (CssLengthValue)((ICssValue)_right).Compute(context);
            var top = (CssLengthValue)((ICssValue)_top).Compute(context);
            return new CssBorderImageSliceValue(top, right, bottom, left, _filled);
        }

        #endregion
    }
}
