namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBottomColorDeclaration
    {
        public static String Name = PropertyNames.BorderBottomColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderColor,
            PropertyNames.BorderBottom,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BorderBottomColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
