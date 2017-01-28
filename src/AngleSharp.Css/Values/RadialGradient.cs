﻿namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a radial gradient:
    /// http://dev.w3.org/csswg/css-images-3/#radial-gradients
    /// </summary>
    public sealed class RadialGradient : IGradient
    {
        #region Fields

        private readonly GradientStop[] _stops;
        private readonly Point _center;
        private readonly Length _width;
        private readonly Length _height;
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
        public RadialGradient(Boolean circle, Point center, Length width, Length height, SizeMode sizeMode, GradientStop[] stops, Boolean repeating = false)
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
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

        /// <summary>
        /// Gets if the gradient should always be displayed as a circle.
        /// </summary>
        public Boolean IsCircle
        {
            get { return _circle; }
        }

        /// <summary>
        /// Gets the special size mode of the gradient.
        /// </summary>
        public SizeMode Mode
        {
            get { return _sizeMode; }
        }

        /// <summary>
        /// Gets the position of the radial gradient.
        /// </summary>
        public Point Position
        {
            get { return _center; }
        }

        /// <summary>
        /// Gets the horizontal radius.
        /// </summary>
        public Length MajorRadius
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the vertical radius.
        /// </summary>
        public Length MinorRadius
        {
            get { return _height; }
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
        /// Returns the string representation of the radial gradient function.
        /// </summary>
        public override String ToString()
        {
            var fn = _repeating ? FunctionNames.RepeatingRadialGradient : FunctionNames.RadialGradient;
            var isDefault = _center == Point.Center && !_circle && _height == Length.Full && _width == Length.Full && _sizeMode == SizeMode.None;
            var offset = isDefault ? 0 : 1;
            var args = new String[_stops.Length + offset];

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
                else if (_circle)
                {
                    size = _width.ToString();
                }
                else
                {
                    size = String.Concat(_width.ToString(), " ", _height.ToString());
                }

                var parts = new[] 
                {
                    _circle ? CssKeywords.Circle : CssKeywords.Ellipse,
                    size,
                    CssKeywords.At,
                    _center.ToString()
                };
                args[0] = String.Join(" ", parts);
            }
            
            for (var i = 0; i < _stops.Length; i++)
            {
                args[offset++] = _stops[i].ToString();
            }

            return fn.CssFunction(String.Join(", ", args));
        }

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
