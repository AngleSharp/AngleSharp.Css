namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a radial gradient:
    /// http://dev.w3.org/csswg/css-images-3/#radial-gradients
    /// </summary>
    /// <remarks>
    /// Creates a new radial gradient.
    /// </remarks>
    /// <param name="circle">Determines if the radial gradient has to be forced to a circle form.</param>
    /// <param name="center">The center point of the gradient.</param>
    /// <param name="width">The width of the ellipsoid.</param>
    /// <param name="height">The height of the ellipsoid.</param>
    /// <param name="sizeMode">The size mode of the ellipsoid.</param>
    /// <param name="stops">A collection of stops to use.</param>
    /// <param name="repeating">The repeating setting.</param>
    public sealed class CssRadialGradientValue(Boolean circle, CssPoint2D center, ICssValue width, ICssValue height, CssRadialGradientValue.SizeMode sizeMode, ICssValue[] stops, Boolean repeating = false) : ICssGradientFunctionValue, IEquatable<CssRadialGradientValue>
    {
        #region Fields

        private readonly ICssValue[] _stops = stops;
        private readonly CssPoint2D _center = center;
        private readonly ICssValue _width = width;
        private readonly ICssValue _height = height;
        private readonly Boolean _repeating = repeating;
        private readonly Boolean _circle = circle;
        private readonly SizeMode _sizeMode = sizeMode;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => _repeating ? FunctionNames.RepeatingRadialGradient : FunctionNames.RadialGradient;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                var center = CssPoint2D.Center;
                var isDefault = _center.X == center.X && _center.Y == center.Y && !_circle && _height == null && _width == null && _sizeMode == SizeMode.None;
                var args = new List<ICssValue>();

                if (!isDefault)
                {
                    var size = String.Empty;

                    if (_sizeMode != SizeMode.None)
                    {
                        foreach (var pair in Map.RadialGradientSizeModes)
                        {
                            if (pair.Value == _sizeMode)
                            {
                                size = pair.Key;
                                break;
                            }
                        }
                    }
                    else if (_width != null)
                    {
                        if (_circle || _height == null)
                        {
                            size = _width.CssText;
                        }
                        else
                        {
                            size = String.Concat(_width.CssText, " ", _height.CssText);
                        }
                    }

                    args.Add(new CssAnyValue(String.Join(" ", new[]
                    {
                        _circle ? CssKeywords.Circle : CssKeywords.Ellipse,
                        size,
                        CssKeywords.At,
                        _center.CssText
                    })));
                }

                foreach (var stop in _stops)
                {
                    args.Add(stop);
                }

                return [.. args];
            }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(Arguments.Join(", "));

        /// <summary>
        /// Gets if the gradient should always be displayed as a circle.
        /// </summary>
        public Boolean IsCircle => _circle;

        /// <summary>
        /// Gets the special size mode of the gradient.
        /// </summary>
        public SizeMode Mode => _sizeMode;

        /// <summary>
        /// Gets the position of the radial gradient.
        /// </summary>
        public CssPoint2D Position => _center;

        /// <summary>
        /// Gets the horizontal radius.
        /// </summary>
        public ICssValue MajorRadius => _width ?? CssLengthValue.Full;

        /// <summary>
        /// Gets the vertical radius.
        /// </summary>
        public ICssValue MinorRadius => _height ?? CssLengthValue.Full;

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
        public Boolean Equals(CssRadialGradientValue other)
        {
            if (_center.Equals(other._center) && _width.Equals(other._width) && _height.Equals(other._height) && _repeating == other._repeating && _sizeMode == other._sizeMode && _circle == other._circle)
            {
                var count = _stops.Length;

                if (count == other._stops.Length)
                {
                    for (var i = 0; i < count; i++)
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
            }

            return false;
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var center = (CssPoint2D)((ICssValue)_center).Compute(context);
            var width = _width.Compute(context);
            var height = _height.Compute(context);
            var stops = _stops.Select(m => (CssGradientStopValue)((ICssValue)m).Compute(context)).ToArray();
            return new CssRadialGradientValue(_circle, center, width, height, _sizeMode, stops, _repeating);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssRadialGradientValue value && Equals(value);

        #endregion

        #region Sizes

        /// <summary>
        /// Enumeration with special size modes.
        /// </summary>
        public enum SizeMode : byte
        {
            /// <summary>
            /// No special size mode set.
            /// </summary>
            None,
            /// <summary>
            /// Size up to the closest corner.
            /// </summary>
            ClosestCorner,
            /// <summary>
            /// Size up to the closest side.
            /// </summary>
            ClosestSide,
            /// <summary>
            /// Size up to the farthest corner.
            /// </summary>
            FarthestCorner,
            /// <summary>
            /// Size up to the farthest side.
            /// </summary>
            FarthestSide
        }

        #endregion
    }
}
