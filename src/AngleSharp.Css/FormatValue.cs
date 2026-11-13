namespace AngleSharp.Css
{
    using System;
    using System.Globalization;

    static class FormatValue
    {
        public const String DoubleFixedPoint = "0.###################################################################################################################################################################################################################################################################################################################################################";

        private static readonly String Zero = 0.0.ToString(DoubleFixedPoint, CultureInfo.InvariantCulture);
        private static readonly String NegativeZero = (-0.0).ToString(DoubleFixedPoint, CultureInfo.InvariantCulture);

        public static String CssStringify(this Double value)
        {
            // the custom format is parsed on every call, which made serializing a length cost a
            // microsecond; integers (most values) and the usual magnitudes give the same with a
            // standard format (15 significant digits, no exponent in that range)
            var magnitude = Math.Abs(value);

            if (value == 0.0)
            {
                return BitConverter.DoubleToInt64Bits(value) < 0 ? NegativeZero : Zero;
            }

            if (magnitude >= 1e-4 && magnitude < 1e15)
            {
                return value == Math.Floor(value)
                    ? ((Int64)value).ToString(CultureInfo.InvariantCulture)
                    : value.ToString("G15", CultureInfo.InvariantCulture);
            }

            return value.ToString(DoubleFixedPoint, CultureInfo.InvariantCulture);
        }
    }
}