namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS calculated value.
    /// </summary>
    /// <remarks>
    /// Creates a new calc function.
    /// </remarks>
    /// <param name="expression">The argument to use.</param>
    public sealed class CssCalcValue(ICssValue expression) : ICssRawValue, ICssFunctionValue
    {
        #region Fields

        private readonly ICssValue _expression = expression;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Calc;

        /// <summary>
        /// Gets the raw value.
        /// </summary>
        public String Value => _expression.CssText;

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

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context) => _expression.Compute(context);

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => Object.ReferenceEquals(this, other);

        #endregion
    }
}
