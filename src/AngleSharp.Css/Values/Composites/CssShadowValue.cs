namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The shadow class for holding information about a box or text-shadow.
    /// </summary>
    public sealed class CssShadowValue : ICssCompositeValue, IEquatable<CssShadowValue>
    {
        #region Fields

        private readonly Boolean _inset;
        private readonly ICssValue _offsetX;
        private readonly ICssValue _offsetY;
        private readonly ICssValue _blurRadius;
        private readonly ICssValue _spreadRadius;
        private readonly CssColorValue? _color;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS shadow.
        /// </summary>
        /// <param name="inset">If the shadow is an inset.</param>
        /// <param name="offsetX">The x-coordinate offset.</param>
        /// <param name="offsetY">The y-coordinate offset.</param>
        /// <param name="blurRadius">The blur radius of the shadow.</param>
        /// <param name="spreadRadius">The spread radius of the shadow.</param>
        /// <param name="color">The color of the shadow.</param>
        public CssShadowValue(Boolean inset, ICssValue offsetX, ICssValue offsetY, ICssValue blurRadius, ICssValue spreadRadius, CssColorValue? color)
        {
            _inset = inset;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _blurRadius = blurRadius;
            _spreadRadius = spreadRadius;
            _color = color;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var parts = new List<String>();

                if (_inset)
                {
                    parts.Add(CssKeywords.Inset);
                }

                parts.Add(_offsetX.CssText);
                parts.Add(_offsetY.CssText);

                if (_blurRadius != null && !_blurRadius.Equals(CssLengthValue.Zero))
                {
                    parts.Add(_blurRadius.CssText);
                }

                if (_spreadRadius != null && !_spreadRadius.Equals(CssLengthValue.Zero))
                {
                    parts.Add(_spreadRadius.CssText);
                }

                if (_color.HasValue)
                {
                    parts.Add(_color.Value.CssText);
                }

                return String.Join(" ", parts);
            }
        }

        /// <summary>
        /// Gets the color of the shadow.
        /// </summary>
        public CssColorValue Color => _color ?? CssColorValue.Black;

        /// <summary>
        /// Gets the horizontal offset.
        /// </summary>
        public ICssValue OffsetX => _offsetX;

        /// <summary>
        /// Gets the vertical offset.
        /// </summary>
        public ICssValue OffsetY => _offsetY;

        /// <summary>
        /// Gets the blur radius.
        /// </summary>
        public ICssValue BlurRadius => _blurRadius;

        /// <summary>
        /// Gets the spread radius.
        /// </summary>
        public ICssValue SpreadRadius => _spreadRadius;

        /// <summary>
        /// Gets if the shadow is inset.
        /// </summary>
        public Boolean IsInset => _inset;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssShadowValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return comparer.Equals(_blurRadius, other._blurRadius) &&
                       comparer.Equals(_spreadRadius, other._spreadRadius) &&
                       comparer.Equals(_color, other._color) &&
                       _inset == other._inset &&
                       comparer.Equals(_offsetX, other._offsetX) &&
                       comparer.Equals(_offsetY, other._offsetY);
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssShadowValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var offsetX = _offsetX.Compute(context);
            var offsetY = _offsetY.Compute(context);
            var blurRadius = _blurRadius.Compute(context);
            var spreadRadius = _spreadRadius?.Compute(context);
            var color = (CssColorValue)((ICssValue)_color).Compute(context);
            return new CssShadowValue(_inset, offsetX, offsetY, blurRadius, spreadRadius, color);
        }

        #endregion
    }
}
