namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundOriginDeclaration
    {
        public static String Name = PropertyNames.BackgroundOrigin;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = BoxModelConverter.FromList();

        public static ICssValue InitialValue = InitialValues.BackgroundOriginDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
