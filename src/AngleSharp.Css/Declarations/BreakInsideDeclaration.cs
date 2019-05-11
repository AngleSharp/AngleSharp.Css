namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BreakInsideDeclaration
    {
        public static String Name = PropertyNames.BreakInside;

        public static IValueConverter Converter = BreakInsideModeConverter;

        public static ICssValue InitialValue = InitialValues.BreakInsideDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
