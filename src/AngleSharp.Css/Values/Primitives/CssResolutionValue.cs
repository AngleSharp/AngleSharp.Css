namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a resolution value.
    /// </summary>
    public readonly struct CssResolutionValue : IEquatable<CssResolutionValue>, IComparable<CssResolutionValue>, ICssMetricValue
    {
        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new resolution value.
        /// </summary>
        /// <param name="value">The value of the resolution in dppx.</param>
        public CssResolutionValue(Double value)
            : this(value, Unit.Dppx)
        {
        }

        /// <summary>
        /// Creates a new resolution value.
        /// </summary>
        /// <param name="value">The value of the resolution.</param>
        /// <param name="unit">The unit of the resolution.</param>
        public CssResolutionValue(Double value, Unit unit)
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
        /// Gets the value of resolution.
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
                    Unit.Dpcm => UnitNames.Dpcm,
                    Unit.Dpi => UnitNames.Dpi,
                    Unit.Dppx => UnitNames.Dppx,
                    _ => String.Empty,
                };
            }
        }

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            if (_unit != Unit.Dppx)
            {
                var dots = ToDotsPerPixel();
                return new CssResolutionValue(dots, Unit.Dppx);
            }

            return this;
        }

        /// <summary>
        /// Tries to convert the given string to a Resolution.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out CssResolutionValue result)
        {
            var unit = GetUnit(s.CssUnit(out double value));

            if (unit != Unit.None)
            {
                result = new CssResolutionValue(value, unit);
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
                "dpcm" => Unit.Dpcm,
                "dpi" => Unit.Dpi,
                "dppx" => Unit.Dppx,
                _ => Unit.None,
            };
        }

        /// <summary>
        /// Converts the resolution to a per pixel density.
        /// </summary>
        /// <returns>The density in dots per pixels.</returns>
        public Double ToDotsPerPixel()
        {
            if (_unit == Unit.Dpi)
            {
                return _value / 96.0;
            }
            else if (_unit == Unit.Dpcm)
            {
                return _value * 127.0 / (50.0 * 96.0);
            }

            return _value;
        }

        /// <summary>
        /// Converts the resolution to the given unit.
        /// </summary>
        /// <param name="unit">The unit to convert to.</param>
        /// <returns>The density in the given unit.</returns>
        public Double To(Unit unit)
        {
            var value = ToDotsPerPixel();

            if (unit == Unit.Dpi)
            {
                return value * 96.0;
            }
            else if (unit == Unit.Dpcm)
            {
                return value * 50.0 * 96.0 / 127.0;
            }

            return value;
        }

        /// <summary>
        /// Checks if the current resolution equals the given one.
        /// </summary>
        /// <param name="other">The given resolution to check for equality.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssResolutionValue other) => _value == other._value && _unit == other._unit;

        #endregion

        #region Units

        /// <summary>
        /// The various resolution units.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
            /// <summary>
            /// The value is a resolution (dots per in).
            /// </summary>
            Dpi,
            /// <summary>
            /// The value is a resolution (dots per cm).
            /// </summary>
            Dpcm,
            /// <summary>
            /// The value is a resolution (dots per px).
            /// </summary>
            Dppx,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares the current resolution against the given one.
        /// </summary>
        /// <param name="other">The resolution to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(CssResolutionValue other) => ToDotsPerPixel().CompareTo(other.ToDotsPerPixel());

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as CssResolutionValue?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssResolutionValue value && Equals(value);

        /// <summary>
        /// Returns a hash code that defines the current resolution.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
