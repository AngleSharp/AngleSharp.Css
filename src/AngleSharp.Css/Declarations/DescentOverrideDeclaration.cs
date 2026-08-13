namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class DescentOverrideDeclaration
    {
        public static String Name = PropertyNames.DescentOverride;

        public static IValueConverter Converter = FontMetricOverrideConverter;

        public static ICssValue InitialValue = InitialValues.DescentOverrideDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
