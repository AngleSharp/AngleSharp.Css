namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class CaptionSideDeclaration
    {
        public static String Name = PropertyNames.CaptionSide;

        public static IValueConverter Converter = CaptionSideConverter;

        public static ICssValue InitialValue = InitialValues.CaptionSideDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
