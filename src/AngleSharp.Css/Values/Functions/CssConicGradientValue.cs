namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a linear gradient:
    /// https://drafts.csswg.org/css-images-4/#conic-gradients
    /// </summary>
    sealed class CssConicGradientValue : ICssGradientFunctionValue, IEquatable<CssConicGradientValue>
    {
        #region Fields

        private readonly ICssValue[] _stops;
        private readonly ICssValue _center;
        private readonly ICssValue _angle;
        private readonly Boolean _repeating;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new conic gradient.
        /// </summary>
        /// <param name="angle">The angle of the conic gradient.</param>
        /// <param name="center">The center to use.</param>
        /// <param name="stops">The stops to use.</param>
        /// <param name="repeating">Indicates if the gradient is repeating.</param>
        public CssConicGradientValue(ICssValue angle, ICssValue center, ICssValue[] stops, Boolean repeating = false)
        {
            _stops = stops;
            _center = center;
            _angle = angle;
            _repeating = repeating;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => _repeating ? FunctionNames.RepeatingConicGradient : FunctionNames.ConicGradient;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                var args = _stops.Cast<ICssValue>().ToList();

                if (_center != null)
                {
                    args.Insert(0, _center);
                }

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
                var defaultAngle = _angle as CssAngleValue?;
                var defaultPosition = _center as CssPoint2D?;
                var offset = (defaultAngle.HasValue ? 1 : 0) + (defaultPosition.HasValue ? 1 : 0);
                var args = new String[_stops.Length + offset];

                if (defaultAngle.HasValue)
                {
                    args[0] = $"from {defaultAngle.Value.CssText}";
                }

                if (defaultPosition.HasValue)
                {
                    args[offset - 1] = $"at {defaultPosition.Value.CssText}";
                }

                for (var i = 0; i < _stops.Length; i++)
                {
                    args[offset++] = _stops[i].CssText;
                }

                return Name.CssFunction(String.Join(", ", args));
            }
        }

        /// <summary>
        /// Gets the angle of the conic gradient.
        /// </summary>
        public ICssValue Angle => _angle ?? Values.CssAngleValue.Half;

        /// <summary>
        /// Gets the position of the conic gradient.
        /// </summary>
        public ICssValue Center => _center ?? CssPoint2D.Center;

        /// <summary>
        /// Gets all stops.
        /// </summary>
        public ICssValue[] Stops => _stops;

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        public Boolean IsRepeating => _repeating;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssConicGradientValue other)
        {
            var l = _stops.Length;

            if (_angle.Equals(other._angle) && _center.Equals(other._center) && _repeating == other._repeating && l == other._stops.Length)
            {
                for (var i = 0; i < l; i++)
                {
                    var a = _stops[i];
                    var b = other._stops[i];

                    if (!a.Equals(b))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssConicGradientValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var center = _center.Compute(context);
            var angle = _angle.Compute(context);
            var stops = _stops.Select(m => (CssGradientStopValue)((ICssValue)m).Compute(context)).ToArray();
            return new CssConicGradientValue(angle, center, stops, _repeating);
        }

        #endregion
    }
}
