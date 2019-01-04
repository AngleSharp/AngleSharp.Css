namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a calc subtraction expression, i.e., a - b.
    /// </summary>
    class CalcSubExpression : ICssValue
    {
        private readonly ICssValue _left;
        private readonly ICssValue _right;

        /// <summary>
        /// Creates a new calc subtraction expression.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public CalcSubExpression(ICssValue left, ICssValue right)
        {
            _left = left;
            _right = right;
        }

        /// <summary>
        /// Gets the left operand.
        /// </summary>
        public ICssValue Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets the right operand.
        /// </summary>
        public ICssValue Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return String.Concat(_left.CssText, " - ", _right.CssText); }
        }
    }
}
