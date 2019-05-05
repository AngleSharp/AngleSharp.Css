namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS label ("string") value.
    /// </summary>
    struct Label : ICssPrimitiveValue
    {
        #region Fields

        private readonly String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS label value.
        /// </summary>
        /// <param name="value">The string to represent.</param>
        public Label(String value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the label value.
        /// </summary>
        public String Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _value.CssString();

        #endregion
    }
}
