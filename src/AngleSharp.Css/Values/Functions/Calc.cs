namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS calculated value.
    /// </summary>
    class Calc : ICssRawValue, ICssFunctionValue
    {
        #region Fields

        private readonly ICssValue _expression;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new calc function.
        /// </summary>
        /// <param name="expression">The argument to use.</param>
        public Calc(ICssValue expression)
        {
            _expression = expression;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Calc;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[] { _expression };

        /// <summary>
        /// Gets the argument of the calc function.
        /// </summary>
        public ICssValue Expression => _expression;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_expression.CssText);

        #endregion
    }
}
