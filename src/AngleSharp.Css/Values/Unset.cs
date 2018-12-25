namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS unset value.
    /// </summary>
    public struct Unset<T> : ICssValue
    {
        private readonly T _value;

        /// <summary>
        /// Creates a new unset value using the given value.
        /// </summary>
        /// <param name="value">The value to unset to.</param>
        public Unset(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the unset value.
        /// </summary>
        public T Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return CssKeywords.Unset; }
        }
    }
}
