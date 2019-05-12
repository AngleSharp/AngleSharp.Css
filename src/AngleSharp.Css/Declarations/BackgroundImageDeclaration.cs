namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundImageDeclaration
    {
        public static String Name = PropertyNames.BackgroundImage;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = MultipleImageSourceConverter;

        public static ICssValue InitialValue = InitialValues.BackgroundImageDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
