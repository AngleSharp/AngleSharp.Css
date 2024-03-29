namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS unset value.
    /// </summary>
    readonly struct CssUnsetValue : ICssSpecialValue
    {
        #region Fields

        private readonly ICssValue _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unset value using the given value.
        /// </summary>
        /// <param name="value">The value to unset to.</param>
        public CssUnsetValue(ICssValue value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unset value.
        /// </summary>
        public ICssValue Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Unset;

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssUnsetValue o && EqualityComparer<ICssValue>.Default.Equals(_value, o.Value);

        #endregion
    }
}
