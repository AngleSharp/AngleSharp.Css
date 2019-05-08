namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents an unknown (any) value.
    /// </summary>
    class CssAnyValue : ICssRawValue
    {
        #region Fields

        private readonly String _text;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unknown value with the given literal content.
        /// </summary>
        /// <param name="text">The serialized value representation..</param>
        public CssAnyValue(String text)
        {
            _text = text;
        }

        #endregion

        #region ctor

        /// <summary>
        /// Gets the contained value. This is the same as CssText.
        /// </summary>
        public String Value => _text;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _text;

        #endregion
    }
}
