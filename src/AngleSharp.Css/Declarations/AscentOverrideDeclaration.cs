namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AscentOverrideDeclaration
    {
        public static String Name = PropertyNames.AscentOverride;

        public static IValueConverter Converter = FontMetricOverrideConverter;

        public static ICssValue InitialValue = InitialValues.AscentOverrideDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
