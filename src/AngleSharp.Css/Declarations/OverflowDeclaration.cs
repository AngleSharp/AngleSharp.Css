namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverflowDeclaration
    {
        public static String Name = PropertyNames.Overflow;

        public static IValueConverter Converter = OverflowModeConverter;

        public static ICssValue InitialValue = InitialValues.OverflowDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
