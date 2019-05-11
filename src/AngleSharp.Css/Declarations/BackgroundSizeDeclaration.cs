namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundSizeDeclaration
    {
        public static String Name = PropertyNames.BackgroundSize;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = BackgroundSizeConverter.FromList();

        public static ICssValue InitialValue = InitialValues.BackgroundSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
