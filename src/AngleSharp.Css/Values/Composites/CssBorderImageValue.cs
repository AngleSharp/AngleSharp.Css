namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS border image definition.
    /// </summary>
    /// <remarks>
    /// Creates a new border image definition.
    /// </remarks>
    /// <param name="image">The image source to use.</param>
    /// <param name="slice">The image slice portion.</param>
    /// <param name="widths">The image width definitions.</param>
    /// <param name="outsets">The image outset declarations.</param>
    /// <param name="repeat">The image repeat settings.</param>
    sealed class CssBorderImageValue(ICssValue image, ICssValue slice, ICssValue widths, ICssValue outsets, ICssValue repeat) : ICssCompositeValue, IEquatable<CssBorderImageValue>
    {
        #region Fields

        private readonly ICssValue _image = image;
        private readonly ICssValue _slice = slice;
        private readonly ICssValue _widths = widths;
        private readonly ICssValue _outsets = outsets;
        private readonly ICssValue _repeat = repeat;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                if (_image != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_image.CssText);
                }

                if (_slice != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_slice.CssText);
                }

                if (_widths != null)
                {
                    sb.Append(" / ").Append(_widths.CssText);
                }

                if (_outsets != null)
                {
                    if (_widths == null) sb.Append(" / ");
                    sb.Append(" / ").Append(_outsets.CssText);
                }

                if (_repeat != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_repeat.CssText);
                }

                return sb.ToPool();
            }
        }

        /// <summary>
        /// Gets the associated image value.
        /// </summary>
        public ICssValue Image => _image;

        /// <summary>
        /// Gets the associated slice value.
        /// </summary>
        public ICssValue Slice => _slice;

        /// <summary>
        /// Gets the associated width value.
        /// </summary>
        public ICssValue Widths => _widths;

        /// <summary>
        /// Gets the associated outset value.
        /// </summary>
        public ICssValue Outsets => _outsets;

        /// <summary>
        /// Gets the associated repeat value.
        /// </summary>
        public ICssValue Repeat => _repeat;

        #endregion

        #region

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssBorderImageValue other)
        {
            return _image.Equals(other._image) &&
                _slice.Equals(other._slice) &&
                _widths.Equals(other._widths) &&
                _outsets.Equals(other._outsets) &&
                _repeat.Equals(other._repeat);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssBorderImageValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var image = _image.Compute(context);
            var slice = _slice.Compute(context);
            var widths = _widths.Compute(context);
            var offsets = _outsets.Compute(context);
            var repeat = _repeat.Compute(context);
            return new CssBorderImageValue(image, slice, widths, offsets, repeat);
    }

        #endregion
    }
}
