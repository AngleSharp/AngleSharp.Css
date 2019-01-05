namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontStyleDeclaration
    {
        public static String Name = PropertyNames.FontStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = Or(FontStyleConverter, AssignInitial(FontStyle.Normal));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
