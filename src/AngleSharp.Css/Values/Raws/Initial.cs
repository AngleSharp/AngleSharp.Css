namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS initial value.
    /// </summary>
    struct Initial<T> : ICssValue
    {
        #region Fields

        private readonly T _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new initial value using the provided data.
        /// </summary>
        /// <param name="value">The initial data.</param>
        public Initial(T value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the initial value.
        /// </summary>
        public T Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Initial;

        #endregion
    }
}
