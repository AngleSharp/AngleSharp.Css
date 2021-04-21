namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a calc bracket expression, i.e., (2+3).
    /// </summary>
    sealed class CssCalcBracketExpression : ICssCompositeValue
    {
        private readonly ICssValue _content;

        /// <summary>
        /// Creates a new calc bracket expression.
        /// </summary>
        /// <param name="content">The enclosed content.</param>
        public CssCalcBracketExpression(ICssValue content)
        {
            _content = content;
        }

        /// <summary>
        /// Gets the bracket's content.
        /// </summary>
        public ICssValue Value => _content;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat("(", _content.CssText, ")");
    }
}
