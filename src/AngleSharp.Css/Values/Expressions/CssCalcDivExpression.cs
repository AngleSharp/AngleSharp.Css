namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a calc division expression, i.e., a / b.
    /// </summary>
    sealed class CssCalcDivExpression : ICssCompositeValue
    {
        private readonly ICssValue _left;
        private readonly ICssValue _right;

        /// <summary>
        /// Creates a new calc division expression.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public CssCalcDivExpression(ICssValue left, ICssValue right)
        {
            _left = left;
            _right = right;
        }

        /// <summary>
        /// Gets the left operand.
        /// </summary>
        public ICssValue Left => _left;

        /// <summary>
        /// Gets the right operand.
        /// </summary>
        public ICssValue Right => _right;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_left.CssText, " / ", _right.CssText);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var left = _left.Compute(context);
            var right = _right.Compute(context);

            if (left is ICssMetricValue x && right is ICssMetricValue y && x.UnitString == y.UnitString)
            {
                var result = x.Value / y.Value;
                return (ICssValue)Activator.CreateInstance(x.GetType(), result);
            }

            // TOOD INVALID
            return this;
        }
    }
}
