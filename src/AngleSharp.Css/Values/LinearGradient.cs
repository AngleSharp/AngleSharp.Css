namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a linear gradient:
    /// http://dev.w3.org/csswg/css-images-3/#linear-gradients
    /// </summary>
    public sealed class LinearGradient : IGradient
    {
        #region Fields

        private readonly GradientStop[] _stops;
        private readonly Angle _angle;
        private readonly Boolean _repeating;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new linear gradient.
        /// </summary>
        /// <param name="angle">The angle of the linear gradient.</param>
        /// <param name="stops">The stops to use.</param>
        /// <param name="repeating">Indicates if the gradient is repeating.</param>
        public LinearGradient(Angle angle, GradientStop[] stops, Boolean repeating = false)
        {
            _stops = stops;
            _angle = angle;
            _repeating = repeating;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

        /// <summary>
        /// Gets the angle of the linear gradient.
        /// </summary>
        public Angle Angle
        {
            get { return _angle; }
        }

        /// <summary>
        /// Gets an enumeration of all stops.
        /// </summary>
        public IEnumerable<GradientStop> Stops
        {
            get { return _stops.AsEnumerable(); }
        }

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        public Boolean IsRepeating
        {
            get { return _repeating; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the string representation of the linear gradient function.
        /// </summary>
        public override String ToString()
        {
            var fn = _repeating ? FunctionNames.RepeatingLinearGradient : FunctionNames.LinearGradient;
            var defaultAngle = _angle == Angle.Zero;
            var offset = defaultAngle ? 0 : 1;
            var args = new String[_stops.Length + offset];

            if (!defaultAngle)
            {
                foreach (var angle in Map.GradientAngles)
                {
                    if (angle.Value == _angle)
                    {
                        args[0] = angle.Key;
                        break;
                    }
                }

                args[0] = args[0] ?? _angle.ToString();
            }

            for (var i = 0; i < _stops.Length; i++)
            {
                args[offset++] = _stops[i].ToString();
            }

            return fn.CssFunction(String.Join(", ", args));
        }

        #endregion
    }
}
