namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a percentage value.
    /// </summary>
    public readonly struct CssPercentageValue : IEquatable<CssPercentageValue>, IComparable<CssPercentageValue>, ICssMetricValue
    {
        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new percentage value.
        /// </summary>
        /// <param name="value">The value of the percentage (0-100).</param>
        public CssPercentageValue(Double value)
            : this(value, Unit.Percent)
        {
        }

        /// <summary>
        /// Creates a new percentage value.
        /// </summary>
        /// <param name="value">The value of the percentage.</param>
        /// <param name="unit">The unit.</param>
        public CssPercentageValue(Double value, Unit unit)
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
        /// Gets the value of percentage.
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
                    Unit.Percent => UnitNames.Percent,
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
        /// Tries to convert the given string to a percentage.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out CssPercentageValue result)
        {
            var unit = GetUnit(s.CssUnit(out double value));

            if (unit != Unit.None)
            {
                result = new CssPercentageValue(value, unit);
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
                "%" => Unit.Percent,
                _ => Unit.None,
            };
        }

        /// <summary>
        /// Converts the percentage to the given unit.
        /// </summary>
        /// <param name="unit">The unit to convert to.</param>
        /// <returns>The value in the given unit.</returns>
        public Double To(Unit unit) => _value;

        /// <summary>
        /// Checks if the current percentage equals the given one.
        /// </summary>
        /// <param name="other">The given percentage to check for equality.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssPercentageValue other) => _value == other._value && _unit == other._unit;

        #endregion

        #region Units

        /// <summary>
        /// The various percentage units.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
            /// <summary>
            /// The value is a percentage.
            /// </summary>
            Percent,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares the current percentage against the given one.
        /// </summary>
        /// <param name="other">The percentage to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(CssPercentageValue other) => _value.CompareTo(other._value);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as CssPercentageValue?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current percentage.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
