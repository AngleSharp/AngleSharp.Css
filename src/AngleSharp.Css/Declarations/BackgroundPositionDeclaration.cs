namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BackgroundPositionDeclaration
    {
        public static String Name = PropertyNames.BackgroundPosition;

        public static String Parent = PropertyNames.Background;

        public static IValueConverter Converter = Or(PointConverter.FromList(), AssignInitial(Point.Center));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
