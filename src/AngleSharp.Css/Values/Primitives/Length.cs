namespace AngleSharp.Css.Values
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents an absolute length value.
    /// </summary>
    struct Length : IEquatable<Length>, IComparable<Length>, ICssPrimitiveValue
    {
        #region Basic lengths

        /// <summary>
        /// Gets a zero pixel length value.
        /// </summary>
        public static readonly Length Zero = new Length(0.0, Unit.Px);

        /// <summary>
        /// Gets the half relative length, i.e. 50%.
        /// </summary>
        public static readonly Length Half = new Length(50.0, Unit.Percent);

        /// <summary>
        /// Gets the full relative length, i.e. 100%.
        /// </summary>
        public static readonly Length Full = new Length(100.0, Unit.Percent);

        /// <summary>
        /// Gets a thin length value.
        /// </summary>
        public static readonly Length Thin = new Length(1.0, Unit.Px);

        /// <summary>
        /// Gets a medium length value.
        /// </summary>
        public static readonly Length Medium = new Length(3.0, Unit.Px);

        /// <summary>
        /// Gets a thick length value.
        /// </summary>
        public static readonly Length Thick = new Length(5.0, Unit.Px);

        /// <summary>
        /// Gets the auto value.
        /// </summary>
        public static readonly Length Auto = new Length(Double.NaN, Unit.Vmax);

        /// <summary>
        /// Gets the content value.
        /// </summary>
        public static readonly Length Content = new Length(Double.NaN, Unit.Percent);

        /// <summary>
        /// Gets the normal value.
        /// </summary>
        public static readonly Length Normal = new Length(Double.NaN, Unit.Em);

        #endregion

        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new length value.
        /// </summary>
        /// <param name="value">The value of the length.</param>
        /// <param name="unit">The unit of the length.</param>
        public Length(Double value, Unit unit)
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
            get
            {
                if (Equals(Auto))
                {
                    return CssKeywords.Auto;
                }
                else if (Equals(Normal))
                {
                    return CssKeywords.Normal;
                }
                else
                {
                    var unit = _value == 0.0 ? String.Empty : UnitString;
                    var val = _value.ToString(CultureInfo.InvariantCulture);
                    return String.Concat(val, unit);
                }
            }
        }

        /// <summary>
        /// Gets if the length is given in absolute units.
        /// Such a length may be converted to pixels.
        /// </summary>
        public Boolean IsAbsolute => _unit == Unit.In || _unit == Unit.Mm || _unit == Unit.Pc || _unit == Unit.Px || _unit == Unit.Pt || _unit == Unit.Cm;

        /// <summary>
        /// Gets if the length is given in relative units.
        /// Such a length cannot be converted to pixels.
        /// </summary>
        public Boolean IsRelative => !IsAbsolute;

        /// <summary>
        /// Gets the type of the length.
        /// </summary>
        public Unit Type => _unit;

        /// <summary>
        /// Gets the value of the length.
        /// </summary>
        public Double Value => _value;

        /// <summary>
        /// Gets the representation of the unit as a string.
        /// </summary>
        public String UnitString
        {
            get
            {
                switch (_unit)
                {
                    case Unit.Px: return UnitNames.Px;
                    case Unit.Em: return UnitNames.Em;
                    case Unit.Ex: return UnitNames.Ex;
                    case Unit.Cm: return UnitNames.Cm;
                    case Unit.Mm: return UnitNames.Mm;
                    case Unit.In: return UnitNames.In;
                    case Unit.Pt: return UnitNames.Pt;
                    case Unit.Pc: return UnitNames.Pc;
                    case Unit.Ch: return UnitNames.Ch;
                    case Unit.Rem: return UnitNames.Rem;
                    case Unit.Vw: return UnitNames.Vw;
                    case Unit.Vh: return UnitNames.Vh;
                    case Unit.Vmin: return UnitNames.Vmin;
                    case Unit.Vmax: return UnitNames.Vmax;
                    case Unit.Percent: return UnitNames.Percent;
                    default: return String.Empty;
                }
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator >=(Length a, Length b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator >(Length a, Length b) => a.CompareTo(b) == 1;

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator <=(Length a, Length b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator <(Length a, Length b) => a.CompareTo(b) == -1;

        /// <summary>
        /// Compares the current length against the given one.
        /// </summary>
        /// <param name="other">The length to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Length other)
        {
            if (_value != 0f || other._value != 0f)
            {
                if (_unit == other._unit)
                {
                    return _value.CompareTo(other._value);
                }
                else if (IsAbsolute && other.IsAbsolute)
                {
                    return ToPixel(null, RenderMode.Undefined).CompareTo(other.ToPixel(null, RenderMode.Undefined));
                }
            }

            return 0;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to convert the given string to a Length.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Length result)
        {
            var unitString = s.CssUnit(out double value);
            var unit = GetUnit(unitString);

            if (unit != Unit.None)
            {
                result = new Length(value, unit);
                return true;
            }
            else if (value == 0.0)
            {
                result = Length.Zero;
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
                case "ch": return Unit.Ch;
                case "cm": return Unit.Cm;
                case "em": return Unit.Em;
                case "ex": return Unit.Ex;
                case "in": return Unit.In;
                case "mm": return Unit.Mm;
                case "pc": return Unit.Pc;
                case "pt": return Unit.Pt;
                case "px": return Unit.Px;
                case "rem": return Unit.Rem;
                case "vh": return Unit.Vh;
                case "vmax": return Unit.Vmax;
                case "vmin": return Unit.Vmin;
                case "vw": return Unit.Vw;
                case "%": return Unit.Percent;
                default: return Unit.None;
            }
        }

        /// <summary>
        /// Converts the length to a number of pixels, if possible. If the
        /// current unit is relative, then an exception will be thrown.
        /// </summary>
        /// <param name="renderDimensions">the render device used to calculate relative units, can be null if units are absolute.</param>
        /// <param name="mode">Signifies the axis the unit represents, use to calculate relative units where the axis matters.</param>
        /// <returns>The number of pixels represented by the current length.</returns>
        public Double ToPixel(IRenderDimensions renderDimensions, RenderMode mode)
        {
            switch (_unit)
            {
                case Unit.In: // 1 in = 2.54 cm
                    return _value * 96.0;
                case Unit.Mm: // 1 mm = 0.1 cm
                    return _value * 5.0 * 96.0 / 127.0;
                case Unit.Pc: // 1 pc = 12 pt
                    return _value * 12.0 * 96.0 / 72.0;
                case Unit.Pt: // 1 pt = 1/72 in
                    return _value * 96.0 / 72.0;
                case Unit.Cm: // 1 cm = 50/127 in
                    return _value * 50.0 * 96.0 / 127.0;
                case Unit.Px: // 1 px = 1/96 in
                    return _value;
                case Unit.Percent:
                    CheckForValidRenderDimensions(renderDimensions, mode);
                    return _value * 0.01 * (mode == RenderMode.Horizontal ? renderDimensions.RenderWidth : renderDimensions.RenderHeight);
                case Unit.Em:
                    CheckForValidRenderDimensionsForFont(renderDimensions);
                    return _value * renderDimensions.FontSize;
                case Unit.Rem:
                    // here we dont actually know the root font size but currently the only IRenderDimensions used is
                    // the IRenderDevice meaning its always the root font size
                    CheckForValidRenderDimensionsForFont(renderDimensions);
                    return _value * renderDimensions.FontSize;
                case Unit.Vh:
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Vertical);
                    return _value * 0.01 * renderDimensions.RenderHeight;
                case Unit.Vw:
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Horizontal);
                    return _value * 0.01 * renderDimensions.RenderWidth;
                case Unit.Vmax:
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Horizontal);
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Vertical);
                    return _value * 0.01 * Math.Max(renderDimensions.RenderHeight, renderDimensions.RenderWidth);
                case Unit.Vmin:
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Horizontal);
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Vertical);
                    return _value * 0.01 * Math.Min(renderDimensions.RenderHeight, renderDimensions.RenderWidth);
                default:
                    throw new InvalidOperationException("Unsupported unit cannot be converted.");
            }
        }

        /// <summary>
        /// Converts the length to the given unit, if possible. If the current
        /// or given unit is relative, then an exception will be thrown.
        /// </summary>
        /// <param name="unit">The unit to convert to.</param>
        /// <param name="renderDimensions">the render device used to calculate relative units, can be null if units are absolute.</param>
        /// <param name="mode">Signifies the axis the unit represents, use to calculate relative units where the axis matters.</param>
        /// <returns>The value in the given unit of the current length.</returns>
        public Double To(Unit unit, IRenderDimensions renderDimensions, RenderMode mode)
        {
            var value = ToPixel(renderDimensions, mode);

            switch (unit)
            {
                case Unit.In: // 1 in = 2.54 cm
                    return value / 96.0;
                case Unit.Mm: // 1 mm = 0.1 cm
                    return value * 127.0 / (5.0 * 96.0);
                case Unit.Pc: // 1 pc = 12 pt
                    return value * 72.0 / (12.0 * 96.0);
                case Unit.Pt: // 1 pt = 1/72 in
                    return value * 72.0 / 96.0;
                case Unit.Cm: // 1 cm = 50/127 in
                    return value * 127.0 / (50.0 * 96.0);
                case Unit.Px: // 1 px = 1/96 in
                    return value;
                case Unit.Percent:
                    CheckForValidRenderDimensions(renderDimensions, mode);
                    return value / (mode == RenderMode.Horizontal ? renderDimensions.RenderWidth : renderDimensions.RenderHeight) * 100;
                case Unit.Em:
                    CheckForValidRenderDimensionsForFont(renderDimensions);
                    return value / renderDimensions.FontSize;
                case Unit.Rem:
                    // here we dont actually know the root font size but currently the only IRenderDimensions used is
                    // the IRenderDevice meaning its always the root font size
                    CheckForValidRenderDimensionsForFont(renderDimensions);
                    return value / renderDimensions.FontSize;
                case Unit.Vh:
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Vertical);
                    return value / (0.01 * renderDimensions.RenderHeight);
                case Unit.Vw:
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Horizontal);
                    return value / ( 0.01 * renderDimensions.RenderWidth);
                case Unit.Vmax:
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Vertical);
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Horizontal);
                    return value / ( 0.01 * Math.Max(renderDimensions.RenderHeight, renderDimensions.RenderWidth));
                case Unit.Vmin:
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Vertical);
                    CheckForValidRenderDimensions(renderDimensions, RenderMode.Horizontal);
                    return value / ( 0.01 * Math.Min(renderDimensions.RenderHeight, renderDimensions.RenderWidth));
                default:
                    throw new InvalidOperationException("Unsupported unit cannot be converted.");
            }
        }

        private void CheckForValidRenderDimensions(IRenderDimensions renderDimensions, RenderMode mode)
        {
            if (renderDimensions == null || mode == RenderMode.Undefined || (mode == RenderMode.Horizontal ? renderDimensions.RenderWidth : renderDimensions.RenderHeight) <= 0)
            {
                throw new ArgumentException("A non null render device with a font size is required to calculate em or rem units.");
            }
        }

        private void CheckForValidRenderDimensionsForFont(IRenderDimensions renderDimensions)
        {
            if (renderDimensions == null || renderDimensions.FontSize <= 0)
            {
                throw new ArgumentException("A non null render device with a font size is required to calculate em or rem units.");
            }
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of length units.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
            /// <summary>
            /// The value is a length (px).
            /// </summary>
            Px,
            /// <summary>
            /// The value is a length (em).
            /// </summary>
            Em,
            /// <summary>
            /// The value is a length (ex). Usually about 0.5em for most fonts.
            /// </summary>
            Ex,
            /// <summary>
            /// The value is a length (cm).
            /// </summary>
            Cm,
            /// <summary>
            /// The value is a length (mm).
            /// </summary>
            Mm,
            /// <summary>
            /// The value is a length (in).
            /// </summary>
            In,
            /// <summary>
            /// The value is a length (pt).
            /// </summary>
            Pt,
            /// <summary>
            /// The value is a length (pc).
            /// </summary>
            Pc,
            /// <summary>
            /// The value is a length (width of the 0-character).
            /// </summary>
            Ch,
            /// <summary>
            /// The value is the relative to the font-size of the root element (in em).
            /// </summary>
            Rem,
            /// <summary>
            /// The value is relative to the viewport width.
            /// 1vw = 1/100 of the viewport width.
            /// </summary>
            Vw,
            /// <summary>
            /// The value is relative to the viewport height.
            /// 1vh = 1/100 of the viewport height.
            /// </summary>
            Vh,
            /// <summary>
            /// The value is relative to the minimum of viewport width and height.
            /// 1vmin = 1/100 of the minimum viewport dimension.
            /// </summary>
            Vmin,
            /// <summary>
            /// The value is relative to the maximum of viewport width and height.
            /// 1vmax = 1/100 of the maximum viewport dimension.
            /// </summary>
            Vmax,
            /// <summary>
            /// The value is relative to a fixed (external) value, that is context
            /// dependent. 1% = 1/100 of the external value.
            /// </summary>
            Percent
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks the equality of the two given lengths.
        /// </summary>
        /// <param name="a">The left length.</param>
        /// <param name="b">The right length.</param>
        /// <returns>True if both lengths are equal, otherwise false.</returns>
        public static Boolean operator ==(Length a, Length b) => a.Equals(b);

        /// <summary>
        /// Checks the inequality of the two given lengths.
        /// </summary>
        /// <param name="a">The left length.</param>
        /// <param name="b">The right length.</param>
        /// <returns>True if both lengths are not equal, otherwise false.</returns>
        public static Boolean operator !=(Length a, Length b) => !a.Equals(b);

        /// <summary>
        /// Checks if both lengths are actually equal.
        /// </summary>
        /// <param name="other">The other length to compare to.</param>
        /// <returns>True if both lengths are equal, otherwise false.</returns>
        public Boolean Equals(Length other) =>
            (_value == other._value || (Double.IsNaN(_value) && Double.IsNaN(other._value))) &&
            (_value == 0.0 || _unit == other._unit);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Length?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current length.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
