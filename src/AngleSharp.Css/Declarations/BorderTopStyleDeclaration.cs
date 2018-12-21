namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderTopStyleDeclaration
    {
        public static String Name = PropertyNames.BorderTopStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderStyle,
            PropertyNames.BorderTop,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
