namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OutlineStyleDeclaration
    {
        public static String Name = PropertyNames.OutlineStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Outline,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
