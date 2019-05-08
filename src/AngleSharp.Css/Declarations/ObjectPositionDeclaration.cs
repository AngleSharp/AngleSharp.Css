namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ObjectPositionDeclaration
    {
        public static String Name = PropertyNames.ObjectPosition;

        public static IValueConverter Converter = Or(PointConverter, AssignInitial(Point.Center));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
