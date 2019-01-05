namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a time value.
    /// </summary>
    struct Time : IEquatable<Time>, IComparable<Time>, ICssValue
    {
        #region Basic times

        /// <summary>
        /// Gets the zero time.
        /// </summary>
        public static readonly Time Zero = new Time(0.0, Unit.Ms);

        #endregion

        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new time value.
        /// </summary>
        /// <param name="value">The value of the time.</param>
        /// <param name="unit">The unit of the time.</param>
        public Time(Double value, Unit unit)
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
        /// Gets the value of time.
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
                    case Unit.Ms:
                        return UnitNames.Ms;

                    case Unit.S:
                        return UnitNames.S;

                    default:
                        return String.Empty;
                }
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two times.
        /// </summary>
        public static Boolean operator >=(Time a, Time b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two times.
        /// </summary>
        public static Boolean operator >(Time a, Time b)
        {
            return a.CompareTo(b) == 1;
        }

        /// <summary>
        /// Compares the magnitude of two times.
        /// </summary>
        public static Boolean operator <=(Time a, Time b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two times.
        /// </summary>
        public static Boolean operator <(Time a, Time b)
        {
            return a.CompareTo(b) == -1;
        }

        /// <summary>
        /// Compares the current time against the given one.
        /// </summary>
        /// <param name="other">The time to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Time other)
        {
            return ToMilliseconds().CompareTo(other.ToMilliseconds());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to convert the given string to a Time.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Time result)
        {
            var value = default(Double);
            var unit = GetUnit(s.CssUnit(out value));

            if (unit != Unit.None)
            {
                result = new Time(value, unit);
                return true;
            }

            result = default(Time);
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
                case "s": return Unit.S;
                case "ms": return Unit.Ms;
                default: return Unit.None;
            }
        }

        /// <summary>
        /// Converts the value to milliseconds.
        /// </summary>
        /// <returns>The number of milliseconds.</returns>
        public Double ToMilliseconds()
        {
            return _unit == Unit.S ? _value * 1000.0 : _value;
        }

        /// <summary>
        /// Checks if the current time is equal to the other time.
        /// </summary>
        /// <param name="other">The time to compare to.</param>
        /// <returns>True if both represent the same value.</returns>
        public Boolean Equals(Time other)
        {
            return ToMilliseconds() == other.ToMilliseconds();
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of time units.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
            /// <summary>
            /// The value is a time (ms).
            /// </summary>
            Ms,
            /// <summary>
            /// The value is a time (s).
            /// </summary>
            S,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks for equality of two times.
        /// </summary>
        public static Boolean operator ==(Time a, Time b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks for inequality of two times.
        /// </summary>
        public static Boolean operator !=(Time a, Time b)
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
            var other = obj as Time?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current time.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion
    }
}
