namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS value that was born from a shorthand.
    /// </summary>
    /// <remarks>
    /// Creates a CSS child-parent container.
    /// </remarks>
    /// <param name="parent">The reference to the shorthand value.</param>
    /// <param name="value">The value of the child, if any.</param>
    sealed class CssChildValue(ICssValue parent, ICssValue value = null) : ICssValue, IEquatable<CssChildValue>
    {
        #region Fields

        private readonly ICssValue _parent = parent;
        private readonly ICssValue _value = value;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the shorthand.
        /// </summary>
        public ICssValue Parent => _parent;

        /// <summary>
        /// Gets the value of the longhand, if any.
        /// </summary>
        public ICssValue Value => _value;

        /// <summary>
        /// Gets the text representation of the longhand.
        /// </summary>
        public String CssText => _value?.CssText ?? String.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssChildValue other)
        {
            return _parent.Equals(other._parent) && _value.Equals(other._value);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var parent = _parent.Compute(context);
            var value = _value?.Compute(context);
            return new CssChildValue(parent, value);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssChildValue value && Equals(value);

        #endregion
    }
}
