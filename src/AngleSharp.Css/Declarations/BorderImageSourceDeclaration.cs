namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderImageSourceDeclaration
    {
        public static String Name = PropertyNames.BorderImageSource;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = OptionalImageSourceConverter;

        public static ICssValue InitialValue = InitialValues.BorderImageSourceDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
