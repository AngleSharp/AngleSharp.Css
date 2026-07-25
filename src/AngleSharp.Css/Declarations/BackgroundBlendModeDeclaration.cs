namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundBlendModeDeclaration
    {
        public static String Name = PropertyNames.BackgroundBlendMode;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = BackgroundBlendModeConverter;

        public static ICssValue InitialValue = InitialValues.BackgroundBlendModeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
