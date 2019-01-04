namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a default value.
    /// </summary>
    class Default<T> : ICssValue
    {
        #region Fields

        private readonly T _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new default value.
        /// </summary>
        /// <param name="value">The used value.</param>
        public Default(T value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Empty;

        /// <summary>
        /// Gets the default value.
        /// </summary>
        public T Value => _value;

        #endregion
    }
}
