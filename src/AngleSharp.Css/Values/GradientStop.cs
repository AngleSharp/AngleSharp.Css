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
        private readonly Length _locationAbsolute;
        private readonly Percent _locationRelative;
        private readonly State _state;

        #endregion

        #region Stop State

        private enum State
        {
            Absolute,
            Relative,
            Undetermined
        }

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
            _locationRelative = Percent.Zero;
            _state = State.Absolute;
            _locationAbsolute = location;
        }

        /// <summary>
        /// Creates a new gradient stop.
        /// </summary>
        /// <param name="color">The color of the stop.</param>
        /// <param name="location">The location of the stop.</param>
        public GradientStop(Color color, Percent location)
        {
            _color = color;
            _locationRelative = location;
            _state = State.Relative;
            _locationAbsolute = Length.Zero;
        }

        /// <summary>
        /// Creates a new gradient stop.
        /// </summary>
        /// <param name="color">The color of the stop.</param>
        public GradientStop(Color color)
        {
            _color = color;
            _locationRelative = Percent.Zero;
            _state = State.Undetermined;
            _locationAbsolute = Length.Zero;
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
        /// Gets if relative positioning is used.
        /// </summary>
        public Boolean IsRelative
        {
            get { return _state == State.Relative; }
        }

        /// <summary>
        /// Gets if absolute positioning is used.
        /// </summary>
        public Boolean IsAbsolute
        {
            get { return _state == State.Absolute; }
        }

        /// <summary>
        /// Gets if the position is undetermined.
        /// </summary>
        public Boolean IsUndetermined
        {
            get { return _state == State.Undetermined; }
        }

        /// <summary>
        /// Gets the absolute location of the gradient stop.
        /// </summary>
        public Length AbsoluteLocation
        {
            get { return _locationAbsolute; }
        }

        /// <summary>
        /// Gets the relative location of the gradient stop.
        /// </summary>
        public Percent RelativeLocation
        {
            get { return _locationRelative; }
        }

        #endregion
    }
}
