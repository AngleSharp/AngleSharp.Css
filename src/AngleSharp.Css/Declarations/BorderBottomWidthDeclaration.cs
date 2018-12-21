namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderBottomWidthDeclaration
    {
        public static String Name = PropertyNames.BorderBottomWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderBottom,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(Length.Medium));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
