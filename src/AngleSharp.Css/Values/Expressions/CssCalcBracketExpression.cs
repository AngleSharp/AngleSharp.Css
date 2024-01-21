namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a calc bracket expression, i.e., (2+3).
    /// </summary>
    /// <remarks>
    /// Creates a new calc bracket expression.
    /// </remarks>
    /// <param name="content">The enclosed content.</param>
    sealed class CssCalcBracketExpression(ICssValue content) : ICssCompositeValue
    {
        #region Fields

        private readonly ICssValue _content = content;

        #endregion
        
        #region ctor

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
