namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Sets a CSS counter.
    /// </summary>
    /// <remarks>
    /// Specifies a counter value.
    /// </remarks>
    /// <param name="name">The name of the referenced counter.</param>
    /// <param name="value">The new value of the counter.</param>
    public readonly struct CssCounterValue(String name, ICssValue value) : ICssPrimitiveValue, IEquatable<CssCounterValue>
    {
        #region Fields

        private readonly String _name = name;
        private readonly ICssValue _value = value;

        #endregion

        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_name, " ", _value.CssText);

        /// <summary>
        /// Gets the identifier of the counter.
        /// </summary>
        public String Name => _name;

        /// <summary>
        /// Gets the value of the counter.
        /// </summary>
        public ICssValue Value => _value;

        #endregion

        #region Methods

        /// <summary>
        /// Checks the two counter values for equality.
        /// </summary>
        /// <param name="other">The other counter to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssCounterValue other) => Name.Is(other.Name) && _value.Equals(other._value);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssCounterValue value && Equals(value);

        /// <summary>
        /// Checks for equality against the given object,
        /// if the provided object is no counter value the
        /// result is false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object? obj) => obj is CssCounterValue cv && Equals(cv);

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => CssText.GetHashCode();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        /// <summary>
        /// Equality check.
        /// </summary>
        public static bool operator ==(CssCounterValue left, CssCounterValue right) => left.Equals(right);

        /// <summary>
        /// Inequality check.
        /// </summary>
        public static bool operator !=(CssCounterValue left, CssCounterValue right) => !(left == right);

        #endregion
    }
}
