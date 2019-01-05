namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderRightColorDeclaration
    {
        public static String Name = PropertyNames.BorderRightColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderColor,
            PropertyNames.BorderRight,
        };

        public static IValueConverter Converter = Or(CurrentColorConverter, AssignInitial(Color.Transparent));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
