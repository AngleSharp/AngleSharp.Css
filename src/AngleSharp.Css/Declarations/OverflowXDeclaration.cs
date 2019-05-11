namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverflowXDeclaration
    {
        public static String Name = PropertyNames.OverflowX;

        public static IValueConverter Converter = OverflowExtendedModeConverter;

        public static ICssValue InitialValue = InitialValues.OverflowDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
