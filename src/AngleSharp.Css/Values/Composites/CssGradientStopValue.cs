namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// More information can be found at the W3C:
    /// http://dev.w3.org/csswg/css-images-3/#color-stop-syntax
    /// </summary>
    /// <remarks>
    /// Creates a new gradient stop.
    /// </remarks>
    /// <param name="color">The color of the stop.</param>
    /// <param name="location">The location of the stop, if any.</param>
    public sealed class CssGradientStopValue(CssColorValue color, ICssValue location = null) : ICssCompositeValue, IEquatable<CssGradientStopValue>
    {
        #region Fields

        private readonly CssColorValue _color = color;
        private readonly ICssValue _location = location;

        #endregion
        #region ctor

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
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => IsDetermined ? String.Concat(_color.CssText, " ", _location.CssText) : _color.CssText;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssGradientStopValue other)
        {
            return _color.Equals(other._color) && _location.Equals(other._location);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var color = ((ICssValue)_color).Compute(context);
            var location = _location?.Compute(context);
            return new CssGradientStopValue((CssColorValue)color, location);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssGradientStopValue value && Equals(value);

        #endregion
    }
}
