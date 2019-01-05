namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderLeftStyleDeclaration
    {
        public static String Name = PropertyNames.BorderLeftStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderStyle,
            PropertyNames.BorderLeft,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
