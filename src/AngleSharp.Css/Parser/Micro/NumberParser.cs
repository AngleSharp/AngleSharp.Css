namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Represents extensions to for number values.
    /// </summary>
    public static class NumberParser
    {
        /// <summary>
        /// Parses the number (double) value.
        /// </summary>
        public static CssNumberValue? ParseNumber(this StringSource source)
        {
            var unit = source.ParseUnit();

            if (unit != null &&
                unit.Dimension == String.Empty &&
                Double.TryParse(unit.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
            {
                return new CssNumberValue(result);
            }

            return null;
        }

        /// <summary>
        /// Parses the ratio (double) value.
        /// </summary>
        public static CssRatioValue? ParseRatio(this StringSource source)
        {
            var pos = source.Index;
            var top = source.ParseNumber();
            var c = source.SkipGetSkip();
            var bottom = source.ParseNumber();

            if (top.HasValue && bottom.HasValue && c == Symbols.Solidus)
            {
                return new CssRatioValue(top.Value, bottom.Value);
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses the integer (double) value.
        /// </summary>
        public static CssNumberValue? ParseNaturalNumber(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseNumber();

            if (element.HasValue && element.Value.Value >= 0f)
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses the natural number (double) value.
        /// </summary>
        public static CssNumberValue? ParseGreaterOrEqualOneNumber(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseNumber();

            if (element.HasValue && element.Value.Value >= 1f)
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses the natural integer (int) value.
        /// </summary>
        public static CssIntegerValue? ParseNaturalInteger(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseInteger();

            if (element.HasValue && element.Value.IntValue >= 0)
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses the positive integer (int) value.
        /// </summary>
        public static CssIntegerValue? ParsePositiveInteger(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseInteger();

            if (element.HasValue && element.Value.IntValue > 0)
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses the weight (int) value.
        /// </summary>
        public static CssIntegerValue? ParseWeightInteger(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParsePositiveInteger();

            if (element.HasValue && IsWeight(element.Value.IntValue))
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses the binary (int) value.
        /// </summary>
        public static CssIntegerValue? ParseBinary(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseInteger();

            if (element.HasValue)
            {
                var val = element.Value.IntValue;

                if (val == 0 || val == 1)
                {
                    return element;
                }
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses the integer (int) value.
        /// </summary>
        public static CssIntegerValue? ParseInteger(this StringSource source)
        {
            var unit = source.ParseUnit();

            if (unit != null && unit.Dimension == String.Empty)
            {
                var value = unit.Value;

                if (Int32.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
                {
                    return new CssIntegerValue(result);
                }

                var negative = value.StartsWith("-");
                var allNumbers = (negative ? value.Substring(1) : value).All(m => m.IsDigit());

                if (allNumbers)
                {
                    return new CssIntegerValue(negative ? Int32.MinValue : Int32.MaxValue);
                }
            }

            return null;
        }

        private static Boolean IsWeight(Int32 value)
        {
            if (value % 100 == 0)
            {
                var s = value / 100;
                return s > 0 && s < 10;
            }

            return false;
        }
    }
}
