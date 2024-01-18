namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents an angle object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/angle
    /// </summary>
    public readonly struct CssAngleValue : IEquatable<CssAngleValue>, IComparable<CssAngleValue>, ICssMetricValue
    {
        #region Basic angles

        /// <summary>
        /// The zero angle.
        /// </summary>
        public static readonly CssAngleValue Zero = new(0.0, CssAngleValue.Unit.Rad);

        /// <summary>
        /// The 45° angle.
        /// </summary>
        public static readonly CssAngleValue HalfQuarter = new(45.0, CssAngleValue.Unit.Deg);

        /// <summary>
        /// The 90° angle.
        /// </summary>
        public static readonly CssAngleValue Quarter = new(90.0, CssAngleValue.Unit.Deg);

        /// <summary>
        /// The 135° angle.
        /// </summary>
        public static readonly CssAngleValue TripleHalfQuarter = new(135.0, CssAngleValue.Unit.Deg);

        /// <summary>
        /// The 180° angle.
        /// </summary>
        public static readonly CssAngleValue Half = new(180.0, CssAngleValue.Unit.Deg);

        #endregion

        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new angle value.
        /// </summary>
        /// <param name="value">The value of the angle in rad.</param>
        public CssAngleValue(Double value)
            : this(value, Unit.Rad)
        {
        }

        /// <summary>
        /// Creates a new angle value.
        /// </summary>
        /// <param name="value">The value of the angle.</param>
        /// <param name="unit">The unit of the angle.</param>
        public CssAngleValue(Double value, Unit unit)
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
                return _unit switch
                {
                    Unit.Deg => UnitNames.Deg,
                    Unit.Grad => UnitNames.Grad,
                    Unit.Turn => UnitNames.Turn,
                    Unit.Rad => UnitNames.Rad,
                    _ => String.Empty,
                };
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator >=(CssAngleValue a, CssAngleValue b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator >(CssAngleValue a, CssAngleValue b) => a.CompareTo(b) == 1;

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator <=(CssAngleValue a, CssAngleValue b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator <(CssAngleValue a, CssAngleValue b) => a.CompareTo(b) == -1;

        /// <summary>
        /// Compares the current angle against the given one.
        /// </summary>
        /// <param name="other">The angle to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(CssAngleValue other) => ToRadian().CompareTo(other.ToRadian());

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            if (_unit != Unit.Rad)
            {
                var rad = ToRadian();
                return new CssAngleValue(rad, Unit.Rad);
            }

            return this;
        }

        /// <summary>
        /// Tries to convert the given string to an Angle.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out CssAngleValue result)
        {
            var unit = GetUnit(s.CssUnit(out double value));

            if (unit != Unit.None)
            {
                result = new CssAngleValue(value, unit);
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
                "deg" => Unit.Deg,
                "grad" => Unit.Grad,
                "turn" => Unit.Turn,
                "rad" => Unit.Rad,
                _ => Unit.None,
            };
        }

        /// <summary>
        /// Converts the contained value to rad.
        /// </summary>
        /// <returns>The value in rad.</returns>
        public Double ToRadian()
        {
            return _unit switch
            {
                Unit.Deg => Math.PI / 180.0 * _value,
                Unit.Grad => Math.PI / 200.0 * _value,
                Unit.Turn => 2.0 * Math.PI * _value,
                _ => _value,
            };
        }

        /// <summary>
        /// Converts the contained value to turns.
        /// </summary>
        /// <returns>The value in turns.</returns>
        public Double ToTurns()
        {
            return _unit switch
            {
                Unit.Deg => _value / 360.0,
                Unit.Grad => _value / 400.0,
                Unit.Rad => _value / (2.0 * Math.PI),
                _ => _value,
            };
        }

        /// <summary>
        /// Converts the contained value to degree.
        /// </summary>
        /// <returns>The value in degree.</returns>
        public Double ToDegree()
        {
            return _unit switch
            {
                Unit.Turn => _value * 360.0,
                Unit.Grad => _value * (360.0 / 400.0),
                Unit.Rad => _value / (180.0 / Math.PI),
                _ => _value,
            };
        }

        /// <summary>
        /// Checks for equality with the other angle.
        /// </summary>
        /// <param name="other">The angle to compare with.</param>
        /// <returns>True if both represent the same angle in rad.</returns>
        public Boolean Equals(CssAngleValue other) => ToRadian() == other.ToRadian();

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
        public static Boolean operator ==(CssAngleValue a, CssAngleValue b) => a.Equals(b);

        /// <summary>
        /// Checks for inequality of two angles.
        /// </summary>
        public static Boolean operator !=(CssAngleValue a, CssAngleValue b) => !a.Equals(b);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as CssAngleValue?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssAngleValue value && Equals(value);

        /// <summary>
        /// Returns a hash code that defines the current angle.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => (Int32)_value;

        #endregion
    }
}
