namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// More information can be found at the W3C:
    /// http://dev.w3.org/csswg/css-images-3/#color-stop-syntax
    /// </summary>
    struct GradientStop : ICssValue
    {
        #region Fields

        private readonly Color _color;
        private readonly ICssValue _location;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new gradient stop.
        /// </summary>
        /// <param name="color">The color of the stop.</param>
        /// <param name="location">The location of the stop, if any.</param>
        public GradientStop(Color color, ICssValue location = null)
        {
            _color = color;
            _location = location;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the gradient stop.
        /// </summary>
        public Color Color => _color;

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
    }
}
