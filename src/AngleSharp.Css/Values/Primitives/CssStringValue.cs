namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS string value.
    /// </summary>
    public readonly struct CssStringValue : ICssPrimitiveValue, IEquatable<CssStringValue>
    {
        #region Fields

        private readonly String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS string value.
        /// </summary>
        /// <param name="value">The string to represent.</param>
        public CssStringValue(String value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the string value.
        /// </summary>
        public String Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _value.CssString();

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        /// <summary>
        /// Checks the two strings for equality.
        /// </summary>
        /// <param name="other">The other string to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssStringValue other) => Value.Is(other.Value);

        /// <summary>
        /// Checks for equality against the given object,
        /// if the provided object is no string the result
        /// is false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is CssStringValue str && Equals(str);

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
