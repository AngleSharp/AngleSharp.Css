namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ListStyleImageDeclaration
    {
        public static String Name = PropertyNames.ListStyleImage;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ListStyle,
        };

        public static IValueConverter Converter = OptionalImageSourceConverter;

        public static ICssValue InitialValue = InitialValues.ListStyleImageDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
