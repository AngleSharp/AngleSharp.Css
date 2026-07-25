namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskBorderDeclaration
    {
        public static String Name = PropertyNames.MaskBorder;

        public static IValueConverter Converter = MaskBorderConverter;

        public static ICssValue InitialValue = InitialValues.MaskBorderDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
