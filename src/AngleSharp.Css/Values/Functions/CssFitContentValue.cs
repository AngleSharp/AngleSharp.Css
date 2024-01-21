namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS fit-content function call.
    /// </summary>
    /// <remarks>
    /// Creates a new fit-content function call.
    /// </remarks>
    /// <param name="dim">The used dimension argument.</param>
    sealed class CssFitContentValue(ICssValue dim) : ICssFunctionValue, IEquatable<CssFitContentValue>
    {
        #region Fields

        private readonly ICssValue _dim = dim;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used dimension.
        /// </summary>
        public ICssValue Dimension => _dim;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.FitContent;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[] { _dim };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_dim.CssText);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssFitContentValue other) => _dim.Equals(other._dim);

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssFitContentValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var dim = _dim.Compute(context);
            return new CssFitContentValue(dim);
        }

        #endregion
    }
}
