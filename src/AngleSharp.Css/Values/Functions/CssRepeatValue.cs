namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS repeat function call.
    /// </summary>
    /// <remarks>
    /// Creates a new repeat function call.
    /// </remarks>
    /// <param name="count">The count value.</param>
    /// <param name="value">The used value.</param>
    sealed class CssRepeatValue(ICssValue count, ICssValue value) : ICssFunctionValue, IEquatable<CssRepeatValue>
    {
        #region Fields

        private readonly ICssValue _count = count;
        private readonly ICssValue _value = value;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used count.
        /// </summary>
        public ICssValue Count => _count;

        /// <summary>
        /// Gets the used value.
        /// </summary>
        public ICssValue Value => _value;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Repeat;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[] { _count, _value };

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
        public Boolean Equals(CssRepeatValue? other)
        {
            return other is not null && _count.Equals(other._count) && _value.Equals(other._value);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssRepeatValue value && Equals(value);

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            var count = _count.Compute(context);
            var value = _value.Compute(context);

            if (count is null || value is null)
            {
                return null;
            }

            if (count != _count || value != _value)
            {
                return new CssRepeatValue(count, value);
            }

            return this;
        }

        #endregion
    }
}
