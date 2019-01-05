namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BackgroundPositionXDeclaration
    {
        public static String Name = PropertyNames.BackgroundPositionX;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BackgroundPosition,
        };

        public static IValueConverter Converter = Or(PointXConverter.FromList(), AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
