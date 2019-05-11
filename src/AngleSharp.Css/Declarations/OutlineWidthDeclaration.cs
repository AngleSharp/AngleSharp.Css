namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OutlineWidthDeclaration
    {
        public static String Name = PropertyNames.OutlineWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Outline,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.OutlineWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
