namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Extensions for the ICssValue interface.
    /// </summary>
    public static class CssValueExtensions
    {
        /// <summary>
        /// Tries to convert the value to a unitless double precision number.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Double AsDouble(this ICssValue? value)
        {
            if (value is null)
            {
                return 0.0;
            }
            else if (value is CssLengthValue l && l.Type == CssLengthValue.Unit.None)
            {
                return l.Value;
            }
            else if (value is CssTimeValue t && t.Type == CssTimeValue.Unit.None)
            {
                return t.Value;
            }
            else if (value is CssFrequencyValue f && f.Type == CssFrequencyValue.Unit.None)
            {
                return f.Value;
            }
            else if (value is CssNumberValue n)
            {
                return n.Value;
            }
            else if (value is CssIntegerValue i)
            {
                return i.Value;
            }
            else if (value is CssPercentageValue p)
            {
                return p.Value;
            }
            else if (value is CssFractionValue fr)
            {
                return fr.Value;
            }
            else if (value is CssRatioValue r)
            {
                return r.Top.AsDouble() / r.Bottom.AsDouble();
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsDouble();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsDouble();
            }

            return 0.0;
        }

        /// <summary>
        /// Tries to convert the value to a number of pixels.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="renderDimensions">the render device used to calculate relative units, can be null if units are absolute.</param>
        /// <param name="mode">Signifies the axis the unit represents, use to calculate relative units where the axis matters.</param>
        /// <returns>The resulting number.</returns>
        public static Double AsPx(this ICssValue? value, IRenderDimensions renderDimensions, RenderMode mode)
        {
            if (value is null)
            {
                return 0.0;
            }
            else if (value is CssLengthValue length && length.Type != CssLengthValue.Unit.None)
            {
                return length.ToPixel(renderDimensions, mode);
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsPx(renderDimensions, mode);
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsPx(renderDimensions, mode);
            }

            return 0.0;
        }

        /// <summary>
        /// Tries to convert the value to a number of milliseconds.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Double AsMs(this ICssValue? value)
        {
            if (value is null)
            {
                return 0.0;
            }
            else if (value is CssTimeValue time && time.Type != CssTimeValue.Unit.None)
            {
                return time.ToMilliseconds();
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsMs();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsMs();
            }

            return 0.0;
        }

        /// <summary>
        /// Tries to convert the value to a number of hertz.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Double AsHz(this ICssValue? value)
        {
            if (value is null)
            {
                return 0.0;
            }
            else if (value is CssFrequencyValue freq && freq.Type != CssFrequencyValue.Unit.None)
            {
                return freq.ToHertz();
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsHz();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsHz();
            }

            return 0.0;
        }

        /// <summary>
        /// Tries to convert the value to a number of degrees.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Double AsDeg(this ICssValue? value)
        {
            if (value is null)
            {
                return 0.0;
            }
            else if (value is CssAngleValue angle && angle.Type != CssAngleValue.Unit.None)
            {
                return angle.ToDegree();
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsDeg();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsDeg();
            }

            return 0.0;
        }

        /// <summary>
        /// Tries to convert the value to a number of degrees.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Double AsRad(this ICssValue? value)
        {
            if (value is null)
            {
                return 0.0;
            }
            else if (value is CssAngleValue angle && angle.Type != CssAngleValue.Unit.None)
            {
                return angle.ToRadian();
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsRad();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsRad();
            }

            return 0.0;
        }

        /// <summary>
        /// Tries to convert the value to a number of dots per inch.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Double AsDpi(this ICssValue? value)
        {
            if (value is null)
            {
                return 0.0;
            }
            else if (value is CssResolutionValue res && res.Type != CssResolutionValue.Unit.None)
            {
                return res.ToDotsPerPixel();
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsDpi();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsDpi();
            }

            return 0.0;
        }

        /// <summary>
        /// Tries to convert the value to an RGBA integer.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Int32 AsRgba(this ICssValue? value)
        {
            if (value is CssColorValue res)
            {
                return res.Value;
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsRgba();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsRgba();
            }

            return CssColorValue.Black.Value;
        }

        /// <summary>
        /// Tries to convert the value to a URL.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting URL.</returns>
        public static String? AsUrl(this ICssValue? value)
        {
            if (value is CssUrlValue res)
            {
                return res.Path;
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsUrl();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsUrl();
            }

            return null;
        }

        /// <summary>
        /// Tries to convert the value to a transformation matrix.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="renderDimensions">the render device used to calculate relative units, can be null if units are absolute.</param>
        /// <returns>The resulting matrix.</returns>
        public static TransformMatrix? AsMatrix(this ICssValue? value, IRenderDimensions renderDimensions)
        {
            if (value is ICssTransformFunctionValue res)
            {
                return res.ComputeMatrix(renderDimensions);
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsMatrix(renderDimensions);
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.AsMatrix(renderDimensions);
            }

            return null;
        }

        /// <summary>
        /// Tries to convert the value to a unitless integer number.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Int32 AsInt32(this ICssValue? value) => (Int32)value.AsDouble();

        /// <summary>
        /// Tries to convert the value to a boolean.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Boolean AsBoolean(this ICssValue? value) => value.AsInt32() != 0;

        /// <summary>
        /// Tries to match the value against a specified enumeration.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The enumeration value or its default value.</returns>
        public static T AsEnum<T>(this ICssValue? value)
            where T : struct, IComparable
        {
            if (value is CssConstantValue<T> constant)
            {
                return constant.Value;
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsEnum<T>();
            }
            else if (value is ICssSpecialValue special && special.Value is not null)
            {
                return special.Value.AsEnum<T>();
            }

            return default;
        }

        /// <summary>
        /// Tries to match the value against a known keyword.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="keyword">The keyword to match.</param>
        /// <returns>True if the keyword was matched, false otherwise.</returns>
        public static Boolean Is(this ICssValue? value, String keyword)
        {
            if (value is CssIdentifierValue ident && ident.Value.Isi(keyword))
            {
                return true;
            }
            else if (value?.GetType() == typeof(CssConstantValue<>) && value.CssText.Isi(keyword))
            {
                return true;
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].Is(keyword);
            }
            else if (value is ICssSpecialValue special && special.Value != null)
            {
                return special.Value.Is(keyword);
            }

            return false;
        }

        /// <summary>
        /// Checks if the two values are equal (either both null, or both same value).
        /// </summary>
        /// <param name="value">The current value.</param>
        /// <param name="other">The value to compare to.</param>
        /// <returns>True if both are equal, otherwise fale.</returns>
        public static Boolean Is(this ICssValue? value, ICssValue? other)
        {
            return Object.ReferenceEquals(value, other) || value is not null && value.Equals(other);
        }
    }
}
