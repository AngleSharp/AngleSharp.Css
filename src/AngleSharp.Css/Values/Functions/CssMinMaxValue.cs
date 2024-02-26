namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS minmax function call.
    /// </summary>
    sealed class CssMinMaxValue : ICssFunctionValue, IEquatable<CssMinMaxValue>
    {
        #region Fields

        private readonly ICssValue _min;
        private readonly ICssValue _max;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new minmax function call.
        /// </summary>
        /// <param name="min">The used lower bound.</param>
        /// <param name="max">The used upper bound.</param>
        public CssMinMaxValue(ICssValue min, ICssValue max)
        {
            _min = min;
            _max = max;
        }

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
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return comparer.Equals(_min, other._min) && comparer.Equals(_max, other._max);
            }

            return false;
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
