namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a calc bracket expression, i.e., (2+3).
    /// </summary>
    sealed class CssCalcBracketExpression : ICssCompositeValue
    {
        #region Fields

        private readonly ICssValue _content;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new calc bracket expression.
        /// </summary>
        /// <param name="content">The enclosed content.</param>
        public CssCalcBracketExpression(ICssValue content)
        {
            _content = content;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bracket's content.
        /// </summary>
        public ICssValue Value => _content;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat("(", _content.CssText, ")");

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context) => _content.Compute(context);

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => Object.ReferenceEquals(this, other);

        #endregion
    }
}
