namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    static class NumberParser
    {
        public static Single? Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseNumber();
            return source.IsDone ? result : null;
        }

        public static Single? ParseNumber(this StringSource source)
        {
            var unit = source.ParseUnit();
            var result = default(Single);

            if (unit != null && unit.Dimension == String.Empty && 
                Single.TryParse(unit.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            return null;
        }

        public static Single? ParseRatio(this StringSource source)
        {
            var pos = source.Index;
            var top = source.ParseNumber();
            var c = source.SkipGetSkip();
            var bottom = source.ParseNumber();

            if (top.HasValue && bottom.HasValue && c == Symbols.Solidus)
            {
                return top.Value / bottom.Value;
            }

            source.BackTo(pos);
            return null;
        }

        public static Single? ParseNaturalNumber(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseNumber();

            if (element.HasValue && element.Value >= 0f)
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        public static Single? ParseGreaterOrEqualOneNumber(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseNumber();

            if (element.HasValue && element.Value >= 1f)
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        public static Int32? ParseNaturalInteger(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseInteger();

            if (element.HasValue && element.Value >= 0)
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        public static Int32? ParsePositiveInteger(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseInteger();

            if (element.HasValue && element.Value > 0)
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        public static Int32? ParseWeightInteger(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParsePositiveInteger();

            if (element.HasValue && IsWeight(element.Value))
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        public static Int32? ParseBinary(this StringSource source)
        {
            var pos = source.Index;
            var element = source.ParseInteger();

            if (element.HasValue && (element.Value == 0 || element.Value == 1))
            {
                return element;
            }

            source.BackTo(pos);
            return null;
        }

        public static Int32? ParseInteger(this StringSource source)
        {
            var unit = source.ParseUnit();
            var result = default(Int32);

            if (unit != null && unit.Dimension == String.Empty &&
                Int32.TryParse(unit.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return result;
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
