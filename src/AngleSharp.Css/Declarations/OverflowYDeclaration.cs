namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverflowYDeclaration
    {
        public static String Name = PropertyNames.OverflowY;

        public static IValueConverter Converter = OverflowExtendedModeConverter;

        public static ICssValue InitialValue = InitialValues.OverflowDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
