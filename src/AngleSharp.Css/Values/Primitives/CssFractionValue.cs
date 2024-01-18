namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a fractional value.
    /// </summary>
    public readonly struct CssFractionValue : IEquatable<CssFractionValue>, IComparable<CssFractionValue>, ICssPrimitiveValue
    {
        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new fractional value.
        /// </summary>
        /// <param name="value">The value of the fraction.</param>
        public CssFractionValue(Double value)
            : this(value, Unit.Fr)
        {
        }

        /// <summary>
        /// Creates a new fractional value.
        /// </summary>
        /// <param name="value">The value of the fraction.</param>
        /// <param name="unit">The unit.</param>
        public CssFractionValue(Double value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_value.CssStringify(), UnitString);

        /// <summary>
        /// Gets the value of fraction.
        /// </summary>
        public Double Value => _value;

        /// <summary>
        /// Gets the type of the length.
        /// </summary>
        public Unit Type => _unit;

        /// <summary>
        /// Gets the representation of the unit as a string.
        /// </summary>
        public String UnitString
        {
            get
            {
                return _unit switch
                {
                    Unit.Fr => UnitNames.Fr,
                    _ => String.Empty,
                };
            }
        }

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        /// <summary>
        /// Tries to convert the given string to a Fraction.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out CssFractionValue result)
        {
            var unit = GetUnit(s.CssUnit(out double value));

            if (unit != Unit.None)
            {
                result = new CssFractionValue(value, unit);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Gets the unit from the enumeration for the provided string.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>A valid CSS unit or None.</returns>
        public static Unit GetUnit(String s)
        {
            return s switch
            {
                "fr" => Unit.Fr,
                _ => Unit.None,
            };
        }

        /// <summary>
        /// Converts the fraction to the given unit.
        /// </summary>
        /// <param name="unit">The unit to convert to.</param>
        /// <returns>The value in the given unit.</returns>
        public Double To(Unit unit) => _value;

        /// <summary>
        /// Checks if the current frequency equals the given one.
        /// </summary>
        /// <param name="other">The given frequency to check for equality.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssFractionValue other) => _value == other._value && _unit == other._unit;

        #endregion

        #region Units

        /// <summary>
        /// The various fractional units.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
            /// <summary>
            /// The value is a fraction.
            /// </summary>
            Fr,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares the current fraction against the given one.
        /// </summary>
        /// <param name="other">The fraction to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(CssFractionValue other) => _value.CompareTo(other._value);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as CssFractionValue?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssFractionValue value && Equals(value);

        /// <summary>
        /// Returns a hash code that defines the current fraction.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
