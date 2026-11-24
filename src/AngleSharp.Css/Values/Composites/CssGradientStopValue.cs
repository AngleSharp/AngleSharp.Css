#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information can be found at the W3C:
    /// http://dev.w3.org/csswg/css-images-3/#color-stop-syntax
    /// </summary>
    public sealed class CssGradientStopValue : ICssCompositeValue, IEquatable<CssGradientStopValue>
    {
        #region Fields

        private readonly CssColorValue _color;
        private readonly ICssValue _location;
        private readonly Boolean _hint;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new gradient stop.
        /// </summary>
        /// <param name="color">The color of the stop.</param>
        /// <param name="location">The location of the stop, if any.</param>
        public CssGradientStopValue(CssColorValue color, ICssValue location = null)
        {
            _color = color;
            _location = location;
        }

        private CssGradientStopValue(ICssValue location, Boolean hint)
        {
            _location = location;
            _hint = hint;
        }

        /// <summary>
        /// Creates a color hint ("red, 20%, blue"): the position between two color stops
        /// where their colors are mixed half and half.
        /// </summary>
        /// <param name="location">The location of the hint.</param>
        /// <returns>The hint, which has no color of its own.</returns>
        public static CssGradientStopValue CreateHint(ICssValue location) => new(location, true);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the gradient stop.
        /// </summary>
        public CssColorValue Color => _color;

        /// <summary>
        /// Gets if the position is determined.
        /// </summary>
        public Boolean IsDetermined => _location != null;

        /// <summary>
        /// Gets if the position is undetermined.
        /// </summary>
        public Boolean IsUndetermined => _location == null;

        /// <summary>
        /// Gets the location of the gradient stop.
        /// </summary>
        public ICssValue Location => _location;

        /// <summary>
        /// Gets if this is a color hint rather than a color stop.
        /// </summary>
        public Boolean IsHint => _hint;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _hint ? _location.CssText : IsDetermined ? String.Concat(_color.CssText, " ", _location.CssText) : _color.CssText;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssGradientStopValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return _hint == other._hint && comparer.Equals(_color, other._color) && comparer.Equals(_location, other._location);
            }

            return false;
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var location = _location?.Compute(context);

            if (_hint)
            {
                return CreateHint(location);
            }

            var color = ((ICssValue)_color).Compute(context);
            return new CssGradientStopValue((CssColorValue)color, location);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssGradientStopValue value && Equals(value);

        #endregion
    }
}
