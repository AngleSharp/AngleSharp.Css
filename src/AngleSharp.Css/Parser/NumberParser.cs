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
    }
}
