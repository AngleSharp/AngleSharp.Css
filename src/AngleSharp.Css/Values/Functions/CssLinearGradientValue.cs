namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a linear gradient:
    /// http://dev.w3.org/csswg/css-images-3/#linear-gradients
    /// </summary>
    sealed class CssLinearGradientValue : ICssGradientFunctionValue, IEquatable<CssLinearGradientValue>
    {
        #region Fields

        private readonly ICssValue[] _stops;
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
        public CssLinearGradientValue(ICssValue angle, ICssValue[] stops, Boolean repeating = false)
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
                var defaultAngle = _angle as CssAngleValue?;
                var offset = defaultAngle.HasValue ? 1 : 0;
                var args = new String[_stops.Length + offset];

                if (defaultAngle.HasValue)
                {
                    var value = defaultAngle.Value;

                    foreach (var angle in Map.GradientAngles)
                    {
                        if (angle.Value == value)
                        {
                            args[0] = angle.Value.CssText;
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
        public ICssValue Angle => _angle ?? Values.CssAngleValue.Half;

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
        public Boolean Equals(CssLinearGradientValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                var l = _stops.Length;

                if (comparer.Equals(_angle, other._angle) && _repeating == other._repeating && l == other._stops.Length)
                {
                    for (var i = 0; i < l; i++)
                    {
                        var a = _stops[i];
                        var b = other._stops[i];

                        if (!comparer.Equals(a, b))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssLinearGradientValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var angle = _angle.Compute(context);
            var stops = _stops.Select(m => (CssGradientStopValue)((ICssValue)m).Compute(context)).ToArray();
            return new CssLinearGradientValue(angle, stops, _repeating);
        }

        #endregion
    }
}
