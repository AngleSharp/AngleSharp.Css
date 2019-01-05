namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class OutlineWidthDeclaration
    {
        public static String Name = PropertyNames.OutlineWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Outline,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(Length.Medium));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
