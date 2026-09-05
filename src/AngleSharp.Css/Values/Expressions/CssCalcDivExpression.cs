#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a calc division expression, i.e., a / b.
    /// </summary>
    sealed class CssCalcDivExpression : ICssCompositeValue
    {
        #region Fields

        private readonly ICssValue _left;
        private readonly ICssValue _right;

        #endregion

        #region ctor

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

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var left = ComputeValue(_left, context);
            var right = ComputeValue(_right, context);

            if (left is ICssMetricValue x && right is ICssMetricValue y)
            {
                // Dividing by a plain number scales the left operand, keeping its unit.
                if (y.UnitString.Length == 0)
                {
                    return x.WithValue(x.Value / y.Value);
                }

                // Dividing two values sharing a unit cancels the unit out, i.e. the
                // result is a plain number (calc(40px / 20px) is 2, not 2px).
                if (x.UnitString == y.UnitString)
                {
                    return new CssLengthValue(x.Value / y.Value, CssLengthValue.Unit.None);
                }
            }

            return null;
        }

        private static ICssValue ComputeValue(ICssValue value, ICssComputeContext context)
        {
            return value is CssLengthValue length && length.Type == CssLengthValue.Unit.None ? value : value.Compute(context);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => Object.ReferenceEquals(this, other);

        #endregion
    }
}
