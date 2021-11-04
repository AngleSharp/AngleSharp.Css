namespace AngleSharp.Css
{
    using System;
    using System.Globalization;

    static class FormatValue
    {
        public const String DoubleFixedPoint = "0.###################################################################################################################################################################################################################################################################################################################################################";

        public static String CssStringify(this Double value) => value.ToString(DoubleFixedPoint, CultureInfo.InvariantCulture);
    }
}