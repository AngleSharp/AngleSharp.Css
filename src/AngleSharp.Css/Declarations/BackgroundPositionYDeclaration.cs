namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BackgroundPositionYDeclaration
    {
        public static String Name = PropertyNames.BackgroundPositionY;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BackgroundPosition,
        };

        public static IValueConverter Converter = Or(PointYConverter.FromList(), AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
