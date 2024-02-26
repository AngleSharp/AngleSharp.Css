namespace AngleSharp.Css
{
    using AngleSharp.Common;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// Convenient CSS-driven extensions for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Serializes the string to a CSS color.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The CSS color representation.</returns>
        public static String CssColor(this String value)
        {
            if (CssColorValue.TryFromHex(value, out var color))
            {
                return color.CssText;
            }

            return value;
        }

        /// <summary>
        /// Parses the integer according to the rules.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <param name="result">The result of parsing the string.</param>
        /// <returns>The indicator if the string was indeed an integer.</returns>
        public static Boolean CssInteger(this String value, out Int32 result)
        {
            return Int32.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Parses the number according to the rules.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <param name="result">The result of parsing the string.</param>
        /// <returns>The indicator if the string was indeed a number.</returns>
        public static Boolean CssNumber(this String value, out Double result)
        {
            return Double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Splits the string in a numeric value (result) and a unit. Returns
        /// null if the provided string is ill-formatted.
        /// </summary>
        /// <param name="value">The value to split.</param>
        /// <param name="result">The returned numeric value.</param>
        /// <returns>The provided CSS unit or null.</returns>
        public static String CssUnit(this String value, out Double result)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var firstLetter = value.Length;

                while (!value[firstLetter - 1].IsDigit() && --firstLetter > 0)
                {
                    // Intentional empty.
                }

                if (firstLetter > 0 && value.Substring(0, firstLetter).CssNumber(out result))
                {
                    return value.Substring(firstLetter);
                }
            }

            result = default;
            return null;
        }

        /// <summary>
        /// Serializes the string to a CSS url.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The CSS url representation.</returns>
        public static String CssUrl(this String value)
        {
            var argument = value.CssString();
            return Keywords.Url.CssFunction(argument);
        }
    }
}
