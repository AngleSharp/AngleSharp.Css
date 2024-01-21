namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a frequency value.
    /// </summary>
    /// <remarks>
    /// Creates a new frequency value.
    /// </remarks>
    /// <param name="value">The value of the frequency.</param>
    /// <param name="unit">The unit of the frequency.</param>
    public readonly struct CssFrequencyValue(Double value, CssFrequencyValue.Unit unit) : IEquatable<CssFrequencyValue>, IComparable<CssFrequencyValue>, ICssMetricValue
    {
        #region Fields

        private readonly Double _value = value;
        private readonly Unit _unit = unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new frequency value.
        /// </summary>
        /// <param name="value">The value of the frequency in Hz.</param>
        public CssFrequencyValue(Double value)
            : this(value, Unit.Hz)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_value.CssStringify(), UnitString);

        /// <summary>
        /// Gets the value of frequency.
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
                    Unit.Khz => UnitNames.Khz,
                    Unit.Hz => UnitNames.Hz,
                    _ => String.Empty,
                };
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two frequencies.
        /// </summary>
        public static Boolean operator >=(CssFrequencyValue a, CssFrequencyValue b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two frequencies.
        /// </summary>
        public static Boolean operator >(CssFrequencyValue a, CssFrequencyValue b) => a.CompareTo(b) == 1;

        /// <summary>
        /// Compares the magnitude of two frequencies.
        /// </summary>
        public static Boolean operator <=(CssFrequencyValue a, CssFrequencyValue b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two frequencies.
        /// </summary>
        public static Boolean operator <(CssFrequencyValue a, CssFrequencyValue b) => a.CompareTo(b) == -1;

        /// <summary>
        /// Compares the current frequency against the given one.
        /// </summary>
        /// <param name="other">The frequency to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(CssFrequencyValue other) => ToHertz().CompareTo(other.ToHertz());

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            if (_unit != Unit.Hz)
            {
                var hz = ToHertz();
                return new CssFrequencyValue(hz, Unit.Hz);
            }

            return this;
        }

        /// <summary>
        /// Tries to convert the given string to a Frequency.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out CssFrequencyValue result)
        {
            var unit = GetUnit(s.CssUnit(out double value));

            if (unit != Unit.None)
            {
                result = new CssFrequencyValue(value, unit);
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
                "hz" => Unit.Hz,
                "khz" => Unit.Khz,
                _ => Unit.None,
            };
        }

        /// <summary>
        /// Converts the value to Hz.
        /// </summary>
        /// <returns>The value in Hz.</returns>
        public Double ToHertz() => _unit == Unit.Khz ? _value * 1000.0 : _value;

        /// <summary>
        /// Checks for equality with the other frequency.
        /// </summary>
        /// <param name="other">The frequency to compare to.</param>
        /// <returns>True if both frequencies are equal, otherwise false.</returns>
        public Boolean Equals(CssFrequencyValue other) => _value == other._value && _unit == other._unit;

        #endregion

        #region Units

        /// <summary>
        /// The various frequency units.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
            /// <summary>
            /// The value is a frequency (Hz).
            /// </summary>
            Hz,
            /// <summary>
            /// The value is a frequency (kHz).
            /// </summary>
            Khz,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks for equality of two frequencies.
        /// </summary>
        public static Boolean operator ==(CssFrequencyValue a, CssFrequencyValue b) => a.Equals(b);

        /// <summary>
        /// Checks for inequality of two frequencies.
        /// </summary>
        public static Boolean operator !=(CssFrequencyValue a, CssFrequencyValue b) => !a.Equals(b);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object? obj) => obj is CssFrequencyValue o && Equals(o.Value);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssFrequencyValue value && Equals(value);

        /// <summary>
        /// Returns a hash code that defines the current frequency.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
