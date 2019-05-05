namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS unset value.
    /// </summary>
    struct Unset<T> : ICssValue
    {
        #region Fields

        private readonly T _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unset value using the given value.
        /// </summary>
        /// <param name="value">The value to unset to.</param>
        public Unset(T value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unset value.
        /// </summary>
        public T Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Unset;

        #endregion
    }
}
