namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS label ("string") value.
    /// </summary>
    struct Label : ICssPrimitiveValue, IEquatable<Label>
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

        #region Methods

        /// <summary>
        /// Checks the two labels for equality.
        /// </summary>
        /// <param name="other">The other label to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(Label other) => Value.Is(other.Value);

        /// <summary>
        /// Checks for equality against the given object,
        /// if the provided object is no label the result
        /// is false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is Label label ? Equals(label) : false;

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
