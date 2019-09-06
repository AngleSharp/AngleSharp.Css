namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents an angle object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/angle
    /// </summary>
    struct Angle : IEquatable<Angle>, IComparable<Angle>, ICssPrimitiveValue
    {
        #region Basic angles

        /// <summary>
        /// The zero angle.
        /// </summary>
        public static readonly Angle Zero = new Angle(0.0, Angle.Unit.Rad);

        /// <summary>
        /// The 45° angle.
        /// </summary>
        public static readonly Angle HalfQuarter = new Angle(45.0, Angle.Unit.Deg);

        /// <summary>
        /// The 90° angle.
        /// </summary>
        public static readonly Angle Quarter = new Angle(90.0, Angle.Unit.Deg);

        /// <summary>
        /// The 135° angle.
        /// </summary>
        public static readonly Angle TripleHalfQuarter = new Angle(135.0, Angle.Unit.Deg);

        /// <summary>
        /// The 180° angle.
        /// </summary>
        public static readonly Angle Half = new Angle(180.0, Angle.Unit.Deg);

        #endregion

        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new angle value.
        /// </summary>
        /// <param name="value">The value of the angle.</param>
        /// <param name="unit">The unit of the angle.</param>
        public Angle(Double value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_value.ToString(CultureInfo.InvariantCulture), UnitString);

        /// <summary>
        /// Gets the value of the angle.
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
                switch (_unit)
                {
                    case Unit.Deg:
                        return UnitNames.Deg;

                    case Unit.Grad:
                        return UnitNames.Grad;

                    case Unit.Turn:
                        return UnitNames.Turn;

                    case Unit.Rad:
                        return UnitNames.Rad;

                    default:
                        return String.Empty;
                }
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator >=(Angle a, Angle b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator >(Angle a, Angle b) => a.CompareTo(b) == 1;

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator <=(Angle a, Angle b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator <(Angle a, Angle b) => a.CompareTo(b) == -1;

        /// <summary>
        /// Compares the current angle against the given one.
        /// </summary>
        /// <param name="other">The angle to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Angle other) => ToRadian().CompareTo(other.ToRadian());

        #endregion

        #region Methods

        /// <summary>
        /// Tries to convert the given string to an Angle.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Angle result)
        {
            var unit = GetUnit(s.CssUnit(out double value));

            if (unit != Unit.None)
            {
                result = new Angle(value, unit);
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
            switch (s)
            {
                case "deg": return Unit.Deg;
                case "grad": return Unit.Grad;
                case "turn": return Unit.Turn;
                case "rad": return Unit.Rad;
                default: return Unit.None;
            }
        }

        /// <summary>
        /// Converts the contained value to rad.
        /// </summary>
        /// <returns>The value in rad.</returns>
        public Double ToRadian()
        {
            switch (_unit)
            {
                case Unit.Deg:
                    return Math.PI / 180.0 * _value;

                case Unit.Grad:
                    return Math.PI / 200.0 * _value;

                case Unit.Turn:
                    return 2.0 * Math.PI * _value;

                default:
                    return _value;
            }
        }

        /// <summary>
        /// Converts the contained value to turns.
        /// </summary>
        /// <returns>The value in turns.</returns>
        public Double ToTurns()
        {
            switch (_unit)
            {
                case Unit.Deg:
                    return _value / 360.0;

                case Unit.Grad:
                    return _value / 400.0;

                case Unit.Rad:
                    return _value / (2.0 * Math.PI);

                default:
                    return _value;
            }
        }

        /// <summary>
        /// Checks for equality with the other angle.
        /// </summary>
        /// <param name="other">The angle to compare with.</param>
        /// <returns>True if both represent the same angle in rad.</returns>
        public Boolean Equals(Angle other) => ToRadian() == other.ToRadian();

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of angle representations.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
            /// <summary>
            /// The value is an angle (deg). There are 360 degrees in a full circle.
            /// </summary>
            Deg,
            /// <summary>
            /// The value is an angle (rad). There are 2*pi radians in a full circle.
            /// </summary>
            Rad,
            /// <summary>
            /// The value is an angle (grad). There are 400 gradians in a full circle.
            /// </summary>
            Grad,
            /// <summary>
            /// The value is a turn. There is 1 turn in a full circle.
            /// </summary>
            Turn,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks for equality of two angles.
        /// </summary>
        public static Boolean operator ==(Angle a, Angle b) => a.Equals(b);

        /// <summary>
        /// Checks for inequality of two angles.
        /// </summary>
        public static Boolean operator !=(Angle a, Angle b) => !a.Equals(b);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Angle?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current angle.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => (Int32)_value;

        #endregion
    }
}
