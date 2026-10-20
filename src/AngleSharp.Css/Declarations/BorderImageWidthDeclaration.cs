namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderImageWidthDeclaration
    {
        public static String Name = PropertyNames.BorderImageWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = BorderImageWidthConverter.BoxRelative();

        public static ICssValue InitialValue = InitialValues.BorderImageWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
