namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS shape.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
    /// </summary>
    public sealed class CssShapeValue : ICssValue, ICssFunctionValue, IEquatable<CssShapeValue>
    {
        #region Fields

        private readonly ICssValue _top;
        private readonly ICssValue _right;
        private readonly ICssValue _bottom;
        private readonly ICssValue _left;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new shape value.
        /// </summary>
        /// <param name="top">The top position.</param>
        /// <param name="right">The right position.</param>
        /// <param name="bottom">The bottom position.</param>
        /// <param name="left">The left position.</param>
        public CssShapeValue(ICssValue top, ICssValue right, ICssValue bottom, ICssValue left)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Rect;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[]
        {
            _top,
            _right,
            _bottom,
            _left,
        };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(Arguments.Join(", "));

        /// <summary>
        /// Gets the top side.
        /// </summary>
        public ICssValue Top => _top;

        /// <summary>
        /// Gets the right side.
        /// </summary>
        public ICssValue Right => _right;

        /// <summary>
        /// Gets the bottom side.
        /// </summary>
        public ICssValue Bottom => _bottom;

        /// <summary>
        /// Gets the left side.
        /// </summary>
        public ICssValue Left => _left;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssShapeValue other)
        {
            return _top.Equals(other._top) && _right.Equals(other._right) && _bottom.Equals(other._bottom) && _left.Equals(other._left);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssShapeValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var top = _top.Compute(context);
            var right = _right.Compute(context);
            var bottom = _bottom.Compute(context);
            var left = _left.Compute(context);
            return new CssShapeValue(top, right, bottom, left);
        }

        #endregion
    }
}
