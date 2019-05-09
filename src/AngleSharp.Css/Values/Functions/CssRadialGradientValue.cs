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
    sealed class CssRadialGradientValue : ICssGradientFunctionValue
    {
        #region Fields

        private readonly CssGradientStopValue[] _stops;
        private readonly Point _center;
        private readonly ICssValue _width;
        private readonly ICssValue _height;
        private readonly Boolean _repeating;
        private readonly Boolean _circle;
        private readonly SizeMode _sizeMode;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new radial gradient.
        /// </summary>
        /// <param name="circle">Determines if the radial gradient has to be forced to a circle form.</param>
        /// <param name="center">The center point of the gradient.</param>
        /// <param name="width">The width of the ellipsoid.</param>
        /// <param name="height">The height of the ellipsoid.</param>
        /// <param name="sizeMode">The size mode of the ellipsoid.</param>
        /// <param name="stops">A collection of stops to use.</param>
        /// <param name="repeating">The repeating setting.</param>
        public CssRadialGradientValue(Boolean circle, Point center, ICssValue width, ICssValue height, SizeMode sizeMode, CssGradientStopValue[] stops, Boolean repeating = false)
        {
            _stops = stops;
            _center = center;
            _width = width;
            _height = height;
            _repeating = repeating;
            _circle = circle;
            _sizeMode = sizeMode;
        }

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
                var isDefault = _center == Point.Center && !_circle && _height == null && _width == null && _sizeMode == SizeMode.None;
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

                return args.ToArray();
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
        public Point Position => _center;

        /// <summary>
        /// Gets the horizontal radius.
        /// </summary>
        public ICssValue MajorRadius => _width ?? Length.Full;

        /// <summary>
        /// Gets the vertical radius.
        /// </summary>
        public ICssValue MinorRadius => _height ?? Length.Full;

        /// <summary>
        /// Gets all stops.
        /// </summary>
        public CssGradientStopValue[] Stops => _stops;

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        public Boolean IsRepeating => _repeating;

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
