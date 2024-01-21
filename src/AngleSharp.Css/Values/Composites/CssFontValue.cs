namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS font definition.
    /// </summary>
    /// <remarks>
    /// Creates a new CSS font definition.
    /// </remarks>
    /// <param name="style">The set style value.</param>
    /// <param name="variant">The set variant value.</param>
    /// <param name="weight">The set weight value.</param>
    /// <param name="stretch">The set stretch value.</param>
    /// <param name="size">The set size value.</param>
    /// <param name="lineHeight">The set line height value.</param>
    /// <param name="fontFamilies">The set families value.</param>
    sealed class CssFontValue(ICssValue style, ICssValue variant, ICssValue weight, ICssValue stretch, ICssValue size, ICssValue lineHeight, ICssValue fontFamilies) : ICssCompositeValue, IEquatable<CssFontValue>
    {
        #region Fields

        private readonly ICssValue _fontFamilies = fontFamilies;
        private readonly ICssValue _lineHeight = lineHeight;
        private readonly ICssValue _size = size;
        private readonly ICssValue _stretch = stretch;
        private readonly ICssValue _style = style;
        private readonly ICssValue _variant = variant;
        private readonly ICssValue _weight = weight;

        #endregion
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the families value.
        /// </summary>
        public ICssValue FontFamilies => _fontFamilies;

        /// <summary>
        /// Gets the line height value.
        /// </summary>
        public ICssValue LineHeight => _lineHeight;

        /// <summary>
        /// Gets the size value.
        /// </summary>
        public ICssValue Size => _size;

        /// <summary>
        /// Gets the stretch value.
        /// </summary>
        public ICssValue Stretch => _stretch;

        /// <summary>
        /// Gets the style value.
        /// </summary>
        public ICssValue Style => _style;

        /// <summary>
        /// Gets the variant value.
        /// </summary>
        public ICssValue Variant => _variant;

        /// <summary>
        /// Gets the weight value.
        /// </summary>
        public ICssValue Weight => _weight;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                if (_style != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_style.CssText);
                }

                if (_variant != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_variant.CssText);
                }

                if (_weight != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_weight.CssText);
                }

                if (_stretch != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_stretch.CssText);
                }

                if (_size != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_size.CssText);
                }

                if (_lineHeight != null)
                {
                    sb.Append(" / ");
                    sb.Append(_lineHeight.CssText);
                }

                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_fontFamilies.CssText);
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
        public Boolean Equals(CssFontValue other)
        {
            return _lineHeight.Equals(other._lineHeight) &&
                _size.Equals(other._size) &&
                _weight.Equals(other._weight) &&
                _stretch.Equals(other._stretch) &&
                _variant.Equals(other._variant) &&
                _style.Equals(other._style) &&
                _fontFamilies.Equals(other._fontFamilies);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssFontValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var fontFamilies = _fontFamilies.Compute(context);
            var lineHeight = _lineHeight.Compute(context);
            var size = _size.Compute(context);
            var stretch = _stretch.Compute(context);
            var style = _style.Compute(context);
            var variant = _variant.Compute(context);
            var weight = _weight.Compute(context);
            return new CssFontValue(style, variant, weight, stretch, size, lineHeight, fontFamilies);
        }

        #endregion
    }
}
