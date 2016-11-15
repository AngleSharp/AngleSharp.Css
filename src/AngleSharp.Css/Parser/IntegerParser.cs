namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    static class IntegerParser
    {
        public static Int32? Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseInteger();
            return source.IsDone ? result : null;
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
    }
}
