namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class LineGapOverrideDeclaration
    {
        public static String Name = PropertyNames.LineGapOverride;

        public static IValueConverter Converter = FontMetricOverrideConverter;

        public static ICssValue InitialValue = InitialValues.LineGapOverrideDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
