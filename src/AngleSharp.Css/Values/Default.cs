namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a default value.
    /// </summary>
    public sealed class Default<T> : ICssValue
    {
        private readonly T _value;

        /// <summary>
        /// Creates a new default value.
        /// </summary>
        /// <param name="value">The used value.</param>
        public Default(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        public T Value
        {
            get { return _value; }
        }
    }
}
