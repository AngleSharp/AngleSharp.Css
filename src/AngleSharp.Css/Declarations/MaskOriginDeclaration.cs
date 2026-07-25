namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskOriginDeclaration
    {
        public static String Name = PropertyNames.MaskOrigin;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Mask,
        };

        public static IValueConverter Converter = MaskOriginConverter;

        public static ICssValue InitialValue = InitialValues.MaskOriginDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
