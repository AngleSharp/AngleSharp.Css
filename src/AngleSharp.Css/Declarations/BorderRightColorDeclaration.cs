namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderRightColorDeclaration
    {
        public static String Name = PropertyNames.BorderRightColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderRight,
            PropertyNames.BorderColor,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BorderRightColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
