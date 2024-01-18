namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a number value.
    /// </summary>
    public readonly struct Number : IEquatable<Number>, IComparable<Number>, ICssMetricValue
    {
        #region Fields

        private readonly Double _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new number value.
        /// </summary>
        /// <param name="value">The value of the number.</param>
        public Number(Double value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _value.CssStringify();

        /// <summary>
        /// Gets the value of the number.
        /// </summary>
        public Double Value => _value;

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
        /// Tries to convert the given string to a number.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Number result)
        {
            if (s.CssNumber(out var value))
            {
                result = new Number(value);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Checks if the current number equals the given one.
        /// </summary>
        /// <param name="other">The given number to check for equality.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(Number other) => _value == other._value;

        #endregion

        #region Equality

        /// <summary>
        /// Compares the current number against the given one.
        /// </summary>
        /// <param name="other">The number to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Number other) => _value.CompareTo(other._value);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Number?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current number.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
