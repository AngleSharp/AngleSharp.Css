namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a cubic-bezier timing-function object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
    /// </summary>
    public sealed class CubicBezierTimingFunction : ITimingFunction, IEquatable<CubicBezierTimingFunction>
    {
        #region Fields

        private readonly Single _x1;
        private readonly Single _y1;
        private readonly Single _x2;
        private readonly Single _y2;

        public static readonly CubicBezierTimingFunction Ease = new CubicBezierTimingFunction(0.25f, 0.1f, 0.25f, 1f);

        public static readonly CubicBezierTimingFunction EaseIn = new CubicBezierTimingFunction(0.42f, 0f, 1f, 1f);

        public static readonly CubicBezierTimingFunction EaseOut = new CubicBezierTimingFunction(0f, 0f, 0.58f, 1f);

        public static readonly CubicBezierTimingFunction EaseInOut = new CubicBezierTimingFunction(0.42f, 0f, 0.58f, 1f);

        public static readonly CubicBezierTimingFunction Linear = new CubicBezierTimingFunction(0f, 0f, 1f, 1f);

        #endregion

        #region ctor

        /// <summary>
        /// The four values specify points P1 and P2 of the curve as (x1, y1, x2, y2). Both
        /// x values must be in the range [0, 1] or the definition is invalid. The y values
        /// can exceed this range.
        /// </summary>
        /// <param name="x1">The x-coordinate of P1.</param>
        /// <param name="y1">The y-coordinate of P1.</param>
        /// <param name="x2">The x-coordinate of P2.</param>
        /// <param name="y2">The y-coordinate of P2.</param>
        public CubicBezierTimingFunction(Single x1, Single y1, Single x2, Single y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
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
        /// Gets the x-coordinate of the p1.
        /// </summary>
        public Single X1
        {
            get { return _x1; }
        }

        /// <summary>
        /// Gets the y-coordinate of the p1.
        /// </summary>
        public Single Y1
        {
            get { return _y1; }
        }

        /// <summary>
        /// Gets the x-coordinate of the p2.
        /// </summary>
        public Single X2
        {
            get { return _x2; }
        }

        /// <summary>
        /// Gets the y-coordinate of the p2.
        /// </summary>
        public Single Y2
        {
            get { return _y2; }
        }

        #endregion

        #region Methods

        public Boolean Equals(CubicBezierTimingFunction other)
        {
            return _x1 == other._x1 && _x2 == other._x2 && _y1 == other._y1 && _y2 == other._y2;
        }

        /// <summary>
        /// Serializes to a string.
        /// </summary>
        public override String ToString()
        {
            if (Equals(Ease))
            {
                return CssKeywords.Ease;
            }
            else if (Equals(EaseIn))
            {
                return CssKeywords.EaseIn;
            }
            else if (Equals(EaseOut))
            {
                return CssKeywords.EaseOut;
            }
            else if (Equals(EaseInOut))
            {
                return CssKeywords.EaseInOut;
            }
            else if (Equals(Linear))
            {
                return CssKeywords.Linear;
            }
            else
            {
                var fn = FunctionNames.CubicBezier;
                var args = new[]
                {
                    _x1.ToString(CultureInfo.InvariantCulture),
                    _y1.ToString(CultureInfo.InvariantCulture),
                    _x2.ToString(CultureInfo.InvariantCulture),
                    _y2.ToString(CultureInfo.InvariantCulture)
                };
                return fn.CssFunction(String.Join(", ", args));
            }
        }

        #endregion
    }
}
