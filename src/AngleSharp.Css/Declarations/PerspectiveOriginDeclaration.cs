namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class PerspectiveOriginDeclaration
    {
        public static String Name = PropertyNames.PerspectiveOrigin;

        public static IValueConverter Converter = Or(PointConverter, AssignInitial(Point.Center));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
