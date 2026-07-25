namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverflowAnchorDeclaration
    {
        public static String Name = PropertyNames.OverflowAnchor;

        public static IValueConverter Converter = OverflowAnchorConverter;

        public static ICssValue InitialValue = InitialValues.OverflowAnchorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
