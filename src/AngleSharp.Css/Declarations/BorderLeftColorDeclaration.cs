namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderLeftColorDeclaration
    {
        public static String Name = PropertyNames.BorderLeftColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderLeft,
            PropertyNames.BorderColor,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BorderLeftColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
