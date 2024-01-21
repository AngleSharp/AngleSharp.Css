namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents an integer value.
    /// </summary>
    /// <remarks>
    /// Creates a new integer value.
    /// </remarks>
    /// <param name="value">The value of the integer.</param>
    public readonly struct CssIntegerValue(Int32 value) : IEquatable<CssIntegerValue>, IComparable<CssIntegerValue>, ICssMetricValue
    {
        #region Basic lengths

        /// <summary>
        /// Gets the 0.0.
        /// </summary>
        public static readonly CssIntegerValue Zero = new(0);

        /// <summary>
        /// Gets the 1.0.
        /// </summary>
        public static readonly CssIntegerValue One = new(1);

        #endregion

        #region Fields

        private readonly Int32 _value = value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new integer value.
        /// </summary>
        /// <param name="value">The value of the integer.</param>
        public CssIntegerValue(Double value)
            : this((Int32)Math.Truncate(value))
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _value.ToString();

        /// <summary>
        /// Gets the value of the integer.
        /// </summary>
        public Double Value => _value;

        /// <summary>
        /// Gets the value of the integer.
        /// </summary>
        public Int32 IntValue => _value;

        /// <summary>
        /// Gets the representation of the unit as a string.
        /// </summary>
        public String UnitString => String.Empty;

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        /// <summary>
        /// Tries to convert the given string to an integer.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out CssIntegerValue result)
        {
            if (s.CssInteger(out var value))
            {
                result = new CssIntegerValue(value);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Checks if the current integer equals the given one.
        /// </summary>
        /// <param name="other">The given integer to check for equality.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssIntegerValue other) => _value == other._value;

        #endregion

        #region Equality

        /// <summary>
        /// Compares the current integer against the given one.
        /// </summary>
        /// <param name="other">The number to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(CssIntegerValue other) => _value.CompareTo(other._value);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object? obj) => obj is CssNumberValue o && Equals(o);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssIntegerValue value && Equals(value);

        /// <summary>
        /// Returns a hash code that defines the current integer.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Equality check.
        /// </summary>
        public static bool operator ==(CssIntegerValue left, CssIntegerValue right) => left.Equals(right);

        /// <summary>
        /// Inequality check.
        /// </summary>
        public static bool operator !=(CssIntegerValue left, CssIntegerValue right) => !(left == right);

        #endregion
    }
}
