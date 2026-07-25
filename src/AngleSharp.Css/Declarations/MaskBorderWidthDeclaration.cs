namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskBorderWidthDeclaration
    {
        public static String Name = PropertyNames.MaskBorderWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.MaskBorder,
        };

        public static IValueConverter Converter = MaskBorderWidthConverter;

        public static ICssValue InitialValue = InitialValues.MaskBorderWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
