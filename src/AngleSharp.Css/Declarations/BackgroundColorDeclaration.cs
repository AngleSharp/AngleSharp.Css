namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundColorDeclaration
    {
        public static String Name = PropertyNames.BackgroundColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BackgroundColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Hashless | PropertyFlags.Animatable;
    }
}
