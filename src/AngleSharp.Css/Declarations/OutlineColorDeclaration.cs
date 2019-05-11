namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OutlineColorDeclaration
    {
        public static String Name = PropertyNames.OutlineColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Outline,
        };

        public static IValueConverter Converter = InvertedColorConverter;

        public static ICssValue InitialValue = InitialValues.OutlineColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
