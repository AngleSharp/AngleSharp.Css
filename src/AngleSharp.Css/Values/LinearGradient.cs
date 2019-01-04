namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a linear gradient:
    /// http://dev.w3.org/csswg/css-images-3/#linear-gradients
    /// </summary>
    class LinearGradient : IGradient, ICssFunctionValue
    {
        #region Fields

        private readonly GradientStop[] _stops;
        private readonly ICssValue _angle;
        private readonly Boolean _repeating;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new linear gradient.
        /// </summary>
        /// <param name="angle">The angle of the linear gradient.</param>
        /// <param name="stops">The stops to use.</param>
        /// <param name="repeating">Indicates if the gradient is repeating.</param>
        public LinearGradient(ICssValue angle, GradientStop[] stops, Boolean repeating = false)
        {
            _stops = stops;
            _angle = angle;
            _repeating = repeating;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => _repeating ? FunctionNames.RepeatingLinearGradient : FunctionNames.LinearGradient;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                var args = _stops.Cast<ICssValue>().ToList();

                if (_angle != null)
                {
                    args.Insert(0, _angle);
                }

                return args.ToArray();
            }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var defaultAngle = _angle as Angle?;
                var offset = defaultAngle.HasValue ? 0 : 1;
                var args = new String[_stops.Length + offset];

                if (defaultAngle.HasValue)
                {
                    var value = defaultAngle.Value;

                    foreach (var angle in Map.GradientAngles)
                    {
                        if (angle.Value == value)
                        {
                            args[0] = angle.Key;
                            break;
                        }
                    }

                    args[0] = args[0] ?? _angle.CssText;
                }

                for (var i = 0; i < _stops.Length; i++)
                {
                    args[offset++] = _stops[i].CssText;
                }

                return Name.CssFunction(String.Join(", ", args));
            }
        }

        /// <summary>
        /// Gets the angle of the linear gradient.
        /// </summary>
        public ICssValue Angle
        {
            get { return _angle ?? Values.Angle.Half; }
        }

        /// <summary>
        /// Gets all stops.
        /// </summary>
        public GradientStop[] Stops
        {
            get { return _stops; }
        }

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        public Boolean IsRepeating
        {
            get { return _repeating; }
        }

        #endregion
    }
}
