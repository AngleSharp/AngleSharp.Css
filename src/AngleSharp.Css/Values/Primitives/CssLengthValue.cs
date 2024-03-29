namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents an absolute length value.
    /// </summary>
    public readonly struct CssLengthValue : IEquatable<CssLengthValue>, IComparable<CssLengthValue>, ICssMetricValue
    {
        #region Basic lengths

        /// <summary>
        /// Gets a zero pixel length value.
        /// </summary>
        public static readonly CssLengthValue Zero = new(0.0, Unit.Px);

        /// <summary>
        /// Gets the half relative length, i.e. 50%.
        /// </summary>
        public static readonly CssLengthValue Half = new(50.0, Unit.Percent);

        /// <summary>
        /// Gets the full relative length, i.e. 100%.
        /// </summary>
        public static readonly CssLengthValue Full = new(100.0, Unit.Percent);

        /// <summary>
        /// Gets a thin length value.
        /// </summary>
        public static readonly CssLengthValue Thin = new(1.0, Unit.Px);

        /// <summary>
        /// Gets a medium length value.
        /// </summary>
        public static readonly CssLengthValue Medium = new(3.0, Unit.Px);

        /// <summary>
        /// Gets a thick length value.
        /// </summary>
        public static readonly CssLengthValue Thick = new(5.0, Unit.Px);

        /// <summary>
        /// Gets the auto value.
        /// </summary>
        public static readonly CssLengthValue Auto = new(Double.NaN, Unit.Vmax);

        /// <summary>
        /// Gets the content value.
        /// </summary>
        public static readonly CssLengthValue Content = new(Double.NaN, Unit.Percent);

        /// <summary>
        /// Gets the normal value.
        /// </summary>
        public static readonly CssLengthValue Normal = new(Double.NaN, Unit.Em);

        #endregion

        #region Fields

        private readonly Double _value;
        private readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new length value in px.
        /// </summary>
        /// <param name="value">The value of the length in px.</param>
        public CssLengthValue(Double value)
            : this(value, Unit.Px)
        {
        }

        /// <summary>
        /// Creates a new length value.
        /// </summary>
        /// <param name="value">The value of the length.</param>
        /// <param name="unit">The unit of the length.</param>
        public CssLengthValue(Double value, Unit unit)
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
                    var val = _value.CssStringify();
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
                return _unit switch
                {
                    Unit.Px => UnitNames.Px,
                    Unit.Em => UnitNames.Em,
                    Unit.Ex => UnitNames.Ex,
                    Unit.Cm => UnitNames.Cm,
                    Unit.Mm => UnitNames.Mm,
                    Unit.In => UnitNames.In,
                    Unit.Pt => UnitNames.Pt,
                    Unit.Pc => UnitNames.Pc,
                    Unit.Ch => UnitNames.Ch,
                    Unit.Rem => UnitNames.Rem,
                    Unit.Vw => UnitNames.Vw,
                    Unit.Vh => UnitNames.Vh,
                    Unit.Vmin => UnitNames.Vmin,
                    Unit.Vmax => UnitNames.Vmax,
                    Unit.Percent => UnitNames.Percent,
                    _ => String.Empty,
                };
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator >=(CssLengthValue a, CssLengthValue b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator >(CssLengthValue a, CssLengthValue b) => a.CompareTo(b) == 1;

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator <=(CssLengthValue a, CssLengthValue b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator <(CssLengthValue a, CssLengthValue b) => a.CompareTo(b) == -1;

        /// <summary>
        /// Compares the current length against the given one.
        /// </summary>
        /// <param name="other">The length to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(CssLengthValue other)
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

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            if (_unit != CssLengthValue.Unit.Px)
            {
                var px = ToPixel(context.Device);
                return new CssLengthValue(px, CssLengthValue.Unit.Px);
            }

            return this;
        }

        /// <summary>
        /// Tries to convert the given string to a Length.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out CssLengthValue result)
        {
            var unitString = s.CssUnit(out double value);
            var unit = GetUnit(unitString);

            if (unit != Unit.None)
            {
                result = new CssLengthValue(value, unit);
                return true;
            }
            else if (value == 0.0)
            {
                result = CssLengthValue.Zero;
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
                "ch" => Unit.Ch,
                "cm" => Unit.Cm,
                "em" => Unit.Em,
                "ex" => Unit.Ex,
                "in" => Unit.In,
                "mm" => Unit.Mm,
                "pc" => Unit.Pc,
                "pt" => Unit.Pt,
                "px" => Unit.Px,
                "rem" => Unit.Rem,
                "vh" => Unit.Vh,
                "vmax" => Unit.Vmax,
                "vmin" => Unit.Vmin,
                "vw" => Unit.Vw,
                "%" => Unit.Percent,
                _ => Unit.None,
            };
        }

        /// <summary>
        /// Converts the length to a number of pixels, if possible. If the
        /// current unit is relative, then an exception will be thrown.
        /// </summary>
        /// <param name="renderDimensions">the render device used to calculate relative units, can be null if units are absolute.</param>
        /// <returns>The number of pixels represented by the current length.</returns>
        public Double ToPixel(IRenderDimensions renderDimensions) => ToPixel(renderDimensions, RenderMode.Horizontal);

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
        public static Boolean operator ==(CssLengthValue a, CssLengthValue b) => a.Equals(b);

        /// <summary>
        /// Checks the inequality of the two given lengths.
        /// </summary>
        /// <param name="a">The left length.</param>
        /// <param name="b">The right length.</param>
        /// <returns>True if both lengths are not equal, otherwise false.</returns>
        public static Boolean operator !=(CssLengthValue a, CssLengthValue b) => !a.Equals(b);

        /// <summary>
        /// Checks if both lengths are actually equal.
        /// </summary>
        /// <param name="other">The other length to compare to.</param>
        /// <returns>True if both lengths are equal, otherwise false.</returns>
        public Boolean Equals(CssLengthValue other) =>
            (_value == other._value || (Double.IsNaN(_value) && Double.IsNaN(other._value))) &&
            (_value == 0.0 || _unit == other._unit);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as CssLengthValue?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssLengthValue value && Equals(value);

        /// <summary>
        /// Returns a hash code that defines the current length.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => _value.GetHashCode();

        #endregion
    }
}
