namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BreakBeforeDeclaration
    {
        public static String Name = PropertyNames.BreakBefore;

        public static IValueConverter Converter = BreakModeConverter;

        public static ICssValue InitialValue = InitialValues.BreakBeforeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
