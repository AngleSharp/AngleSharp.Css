namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a time value.
    /// </summary>
    struct Frequency : IEquatable<Frequency>, IComparable<Frequency>, ICssValue
    {
        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new frequency value.
        /// </summary>
        /// <param name="value">The value of the frequency.</param>
        /// <param name="unit">The unit of the frequency.</param>
        public Frequency(Double value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return String.Concat(_value.ToString(CultureInfo.InvariantCulture), UnitString); }
        }

        /// <summary>
        /// Gets the value of frequency.
        /// </summary>
        public Double Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the type of the length.
        /// </summary>
        public Unit Type
        {
            get { return _unit; }
        }

        /// <summary>
        /// Gets the representation of the unit as a string.
        /// </summary>
        public String UnitString
        {
            get
            {
                switch (_unit)
                {
                    case Unit.Khz:
                        return UnitNames.Khz;

                    case Unit.Hz:
                        return UnitNames.Hz;

                    default:
                        return String.Empty;
                }
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two frequencies.
        /// </summary>
        public static Boolean operator >=(Frequency a, Frequency b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two frequencies.
        /// </summary>
        public static Boolean operator >(Frequency a, Frequency b)
        {
            return a.CompareTo(b) == 1;
        }

        /// <summary>
        /// Compares the magnitude of two frequencies.
        /// </summary>
        public static Boolean operator <=(Frequency a, Frequency b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two frequencies.
        /// </summary>
        public static Boolean operator <(Frequency a, Frequency b)
        {
            return a.CompareTo(b) == -1;
        }

        /// <summary>
        /// Compares the current frequency against the given one.
        /// </summary>
        /// <param name="other">The frequency to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Frequency other)
        {
            return ToHertz().CompareTo(other.ToHertz());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to convert the given string to a Frequency.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Frequency result)
        {
            var value = default(Double);
            var unit = GetUnit(s.CssUnit(out value));

            if (unit != Unit.None)
            {
                result = new Frequency(value, unit);
                return true;
            }

            result = default(Frequency);
            return false;
        }

        /// <summary>
        /// Gets the unit from the enumeration for the provided string.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>A valid CSS unit or None.</returns>
        public static Unit GetUnit(String s)
        {
            switch (s)
            {
                case "hz": return Unit.Hz;
                case "khz": return Unit.Khz;
                default: return Unit.None;
            }
        }

        /// <summary>
        /// Converts the value to Hz.
        /// </summary>
        /// <returns>The value in Hz.</returns>
        public Double ToHertz()
        {
            return _unit == Unit.Khz ? _value * 1000.0 : _value;
        }

        /// <summary>
        /// Checks for equality with the other frequency.
        /// </summary>
        /// <param name="other">The frequency to compare to.</param>
        /// <returns>True if both frequencies are equal, otherwise false.</returns>
        public Boolean Equals(Frequency other)
        {
            return _value == other._value && _unit == other._unit;
        }

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
        public static Boolean operator ==(Frequency a, Frequency b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks for inequality of two frequencies.
        /// </summary>
        public static Boolean operator !=(Frequency a, Frequency b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Frequency?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current frequency.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion
    }
}
