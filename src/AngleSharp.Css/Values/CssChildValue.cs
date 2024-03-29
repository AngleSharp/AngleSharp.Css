namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS value that was born from a shorthand.
    /// </summary>
    sealed class CssChildValue : ICssValue, IEquatable<CssChildValue>
    {
        #region Fields

        private readonly ICssValue _parent;
        private readonly ICssValue _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a CSS child-parent container.
        /// </summary>
        /// <param name="parent">The reference to the shorthand value.</param>
        /// <param name="value">The value of the child, if any.</param>
        public CssChildValue(ICssValue parent, ICssValue value = null)
        {
            _parent = parent;
            _value = value;
        }

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
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return comparer.Equals(_parent, other._parent) && comparer.Equals(_value, other._value);
            }

            return false;
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
