namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// More information can be found at the W3C:
    /// http://dev.w3.org/csswg/css-images-3/#color-stop-syntax
    /// </summary>
    public struct GradientStop
    {
        #region Fields

        private readonly Color _color;
        private readonly Length _location;
        private readonly Boolean _determined;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new gradient stop.
        /// </summary>
        /// <param name="color">The color of the stop.</param>
        /// <param name="location">The location of the stop.</param>
        public GradientStop(Color color, Length location)
        {
            _color = color;
            _location = location;
            _determined = true;
        }

        /// <summary>
        /// Creates a new gradient stop.
        /// </summary>
        /// <param name="color">The color of the stop.</param>
        public GradientStop(Color color)
        {
            _color = color;
            _determined = false;
            _location = Length.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the gradient stop.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        /// <summary>
        /// Gets if the position is determined.
        /// </summary>
        public Boolean IsDetermined
        {
            get { return _determined; }
        }

        /// <summary>
        /// Gets if the position is undetermined.
        /// </summary>
        public Boolean IsUndetermined
        {
            get { return !_determined; }
        }

        /// <summary>
        /// Gets the location of the gradient stop.
        /// </summary>
        public Length Location
        {
            get { return _location; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the string representation of the gradient stop.
        /// </summary>
        public override String ToString()
        {
            return _determined ? 
                String.Concat(_color.ToString(), " ", _location.ToString()) : 
                _color.ToString();
        }

        #endregion
    }
}
