namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS value that was born from a shorthand.
    /// </summary>
    class CssChildValue : ICssValue
    {
        private readonly ICssValue _parent;
        private readonly ICssValue _value;

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

        /// <summary>
        /// Gets the value of the shorthand.
        /// </summary>
        public ICssValue Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the value of the longhand, if any.
        /// </summary>
        public ICssValue Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the text representation of the longhand.
        /// </summary>
        public String CssText
        {
            get { return _value?.CssText ?? String.Empty; }
        }
    }
}
