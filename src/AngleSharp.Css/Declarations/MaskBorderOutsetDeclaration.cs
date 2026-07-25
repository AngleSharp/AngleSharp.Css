namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskBorderOutsetDeclaration
    {
        public static String Name = PropertyNames.MaskBorderOutset;

        public static String[] Shorthands = new[]
        {
            PropertyNames.MaskBorder,
        };

        public static IValueConverter Converter = MaskBorderOutsetConverter;

        public static ICssValue InitialValue = InitialValues.MaskBorderOutsetDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
