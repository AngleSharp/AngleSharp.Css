namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderRightStyleDeclaration
    {
        public static String Name = PropertyNames.BorderRightStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderStyle,
            PropertyNames.BorderRight,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
