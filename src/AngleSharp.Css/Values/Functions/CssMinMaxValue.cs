namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS minmax function call.
    /// </summary>
    /// <remarks>
    /// Creates a new minmax function call.
    /// </remarks>
    /// <param name="min">The used lower bound.</param>
    /// <param name="max">The used upper bound.</param>
    sealed class CssMinMaxValue(ICssValue min, ICssValue max) : ICssFunctionValue, IEquatable<CssMinMaxValue>
    {
        #region Fields

        private readonly ICssValue _min = min;
        private readonly ICssValue _max = max;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the upper limit.
        /// </summary>
        public ICssValue Maximum => _max;

        /// <summary>
        /// Gets the lower limit.
        /// </summary>
        public ICssValue Minimum => _min;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Minmax;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[] { _min, _max };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(Arguments.Join(", "));

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssMinMaxValue other)
        {
            return _min.Equals(other._min) && _max.Equals(other._max);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var min = _min.Compute(context);
            var max = _max.Compute(context);
            return new CssMinMaxValue(min, max);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssMinMaxValue value && Equals(value);

        #endregion
    }
}
