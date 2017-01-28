namespace AngleSharp.Css.Extensions
{
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
            var color = default(Color);

            if (Color.TryFromHex(value, out color))
            {
                return color.CssText;
            }

            return value;
        }

        /// <summary>
        /// Splits the string in a numeric value (result) and a unit. Returns
        /// null if the provided string is ill-formatted.
        /// </summary>
        /// <param name="value">The value to split.</param>
        /// <param name="result">The returned numeric value.</param>
        /// <returns>The provided CSS unit or null.</returns>
        public static String CssUnit(this String value, out Single result)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var firstLetter = value.Length;

                while (!value[firstLetter - 1].IsDigit() && --firstLetter > 0)
                {
                    // Intentional empty.
                }

                if (firstLetter > 0 && Single.TryParse(value.Substring(0, firstLetter), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                {
                    return value.Substring(firstLetter);
                }
            }

            result = default(Single);
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
            return AngleSharp.Common.Keywords.Url.CssFunction(argument);
        }
    }
}
