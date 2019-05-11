namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderTopColorDeclaration
    {
        public static String Name = PropertyNames.BorderTopColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderColor,
            PropertyNames.BorderTop,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BorderTopColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
