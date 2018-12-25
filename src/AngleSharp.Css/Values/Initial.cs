namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS initial value.
    /// </summary>
    public struct Initial<T> : ICssValue
    {
        private readonly T _value;

        /// <summary>
        /// Creates a new initial value using the provided data.
        /// </summary>
        /// <param name="value">The initial data.</param>
        public Initial(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the initial value.
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
            get { return CssKeywords.Initial; }
        }
    }
}
