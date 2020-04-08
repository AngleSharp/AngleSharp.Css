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
        public static Double AsDouble(this ICssValue value)
        {
            if (value is Length length && length.Type == Length.Unit.None)
            {
                return length.Value;
            }
            else if (value is Fraction fr)
            {
                return fr.Value;
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
        public static Double AsPx(this ICssValue value, IRenderDimensions renderDimensions, RenderMode mode)
        {
            if (value is Length length && length.Type != Length.Unit.None)
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
        public static Double AsMs(this ICssValue value)
        {
            if (value is Time time && time.Type != Time.Unit.None)
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
        public static Double AsHz(this ICssValue value)
        {
            if (value is Frequency freq && freq.Type != Frequency.Unit.None)
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
        public static Double AsDeg(this ICssValue value)
        {
            if (value is Angle angle && angle.Type != Angle.Unit.None)
            {
                return angle.Value;
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
        /// Tries to convert the value to a number of dots per inch.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Double AsDpi(this ICssValue value)
        {
            if (value is Resolution res && res.Type != Resolution.Unit.None)
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
        public static Int32 AsRgba(this ICssValue value)
        {
            if (value is Color res)
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

            return Color.Black.Value;
        }

        /// <summary>
        /// Tries to convert the value to a URL.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting URL.</returns>
        public static String AsUrl(this ICssValue value)
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
        public static TransformMatrix AsMatrix(this ICssValue value, IRenderDimensions renderDimensions)
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
        public static Int32 AsInt32(this ICssValue value) => (Int32)value.AsDouble();

        /// <summary>
        /// Tries to convert the value to a boolean.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting number.</returns>
        public static Boolean AsBoolean(this ICssValue value) => value.AsInt32() != 0;

        /// <summary>
        /// Tries to match the value against a specified enumeration.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The enumeration value or its default value.</returns>
        public static T AsEnum<T>(this ICssValue value)
            where T : struct, IComparable
        {
            if (value is Constant<T> constant)
            {
                return constant.Value;
            }
            else if (value is ICssMultipleValue multiple && multiple.Count == 1)
            {
                return multiple[0].AsEnum<T>();
            }
            else if (value is ICssSpecialValue special && special.Value != null)
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
        public static Boolean Is(this ICssValue value, String keyword)
        {
            if (value is Identifier ident && ident.Value.Isi(keyword))
            {
                return true;
            }
            else if (value?.GetType() == typeof(Constant<>) && value.CssText.Isi(keyword))
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
    }
}
