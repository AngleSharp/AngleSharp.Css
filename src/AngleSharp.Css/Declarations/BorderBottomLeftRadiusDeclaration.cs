namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderBottomLeftRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderBottomLeftRadius;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderRadius,
        };

        public static IValueConverter Converter = Or(BorderRadiusLonghandConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
