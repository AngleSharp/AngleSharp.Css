namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderTopWidthDeclaration
    {
        public static String Name = PropertyNames.BorderTopWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderTop,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(Length.Medium));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
