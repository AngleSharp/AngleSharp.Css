namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a cubic-bezier timing-function object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
    /// </summary>
    /// <remarks>
    /// The four values specify points P1 and P2 of the curve as (x1, y1, x2, y2). Both
    /// x values must be in the range [0, 1] or the definition is invalid. The y values
    /// can exceed this range.
    /// </remarks>
    /// <param name="x1">The x-coordinate of P1.</param>
    /// <param name="y1">The y-coordinate of P1.</param>
    /// <param name="x2">The x-coordinate of P2.</param>
    /// <param name="y2">The y-coordinate of P2.</param>
    sealed class CssCubicBezierValue(ICssValue x1, ICssValue y1, ICssValue x2, ICssValue y2) : ICssTimingFunctionValue, IEquatable<CssCubicBezierValue>
    {
        #region Fields

        private readonly ICssValue _x1 = x1;
        private readonly ICssValue _y1 = y1;
        private readonly ICssValue _x2 = x2;
        private readonly ICssValue _y2 = y2;

        /// <summary>
        /// The pre-configured ease function.
        /// </summary>
        public static readonly CssCubicBezierValue Ease = new(0.25, 0.1, 0.25, 1.0);

        /// <summary>
        /// The pre-configured ease-in function.
        /// </summary>
        public static readonly CssCubicBezierValue EaseIn = new(0.42, 0.0, 1.0, 1.0);

        /// <summary>
        /// The pre-configured ease-out function.
        /// </summary>
        public static readonly CssCubicBezierValue EaseOut = new(0.0, 0.0, 0.58, 1.0);

        /// <summary>
        /// The pre-configured ease-in-out function.
        /// </summary>
        public static readonly CssCubicBezierValue EaseInOut = new(0.42, 0.0, 0.58, 1.0);

        /// <summary>
        /// The pre-configured linear function.
        /// </summary>
        public static readonly CssCubicBezierValue Linear = new(0.0, 0.0, 1.0, 1.0);

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
        public CssCubicBezierValue(Double x1, Double y1, Double x2, Double y2)
            : this(new CssNumberValue(x1), new CssNumberValue(y1), new CssNumberValue(x2), new CssNumberValue(y2))
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.CubicBezier;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new ICssValue[]
        {
            _x1,
            _y1,
            _x2,
            _y2,
        };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
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

                return Name.CssFunction(Arguments.Join(", "));
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the p1.
        /// </summary>
        public ICssValue X1 => _x1;

        /// <summary>
        /// Gets the y-coordinate of the p1.
        /// </summary>
        public ICssValue Y1 => _y1;

        /// <summary>
        /// Gets the x-coordinate of the p2.
        /// </summary>
        public ICssValue X2 => _x2;

        /// <summary>
        /// Gets the y-coordinate of the p2.
        /// </summary>
        public ICssValue Y2 => _y2;

        #endregion

        #region Methods

        /// <summary>
        /// Checks with equality to another cubic bezier timing function.
        /// </summary>
        /// <param name="other">The cubic bezier to compare to.</param>
        /// <returns>True if both have the same parameters, otherwise false.</returns>
        public Boolean Equals(CssCubicBezierValue other) =>
            _x1.Equals(other._x1) && _x2.Equals(other._x2) && _y1.Equals(other._y1) && _y2.Equals(other._y2);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var x1 = _x1.Compute(context);
            var x2 = _x2.Compute(context);
            var y1 = _y1.Compute(context);
            var y2 = _y2.Compute(context);
            return new CssCubicBezierValue(x1, y1, x2, y2);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssCubicBezierValue value && Equals(value);

        #endregion
    }
}
