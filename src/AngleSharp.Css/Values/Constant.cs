namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a selected CSS enum value.
    /// </summary>
    class Constant<T> : ICssValue
    {
        #region Fields

        private readonly String _key;
        private readonly T _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new selected CSS enum value.
        /// </summary>
        /// <param name="key">The key representation.</param>
        /// <param name="data">The associated data.</param>
        public Constant(String key, T data)
        {
            _key = key;
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated value.
        /// </summary>
        public T Value => _data;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _key;

        #endregion
    }
}
