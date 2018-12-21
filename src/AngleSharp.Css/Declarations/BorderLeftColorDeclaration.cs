namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderLeftColorDeclaration
    {
        public static String Name = PropertyNames.BorderLeftColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderColor,
            PropertyNames.BorderLeft,
        };

        public static IValueConverter Converter = Or(CurrentColorConverter, AssignInitial(Color.Transparent));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
