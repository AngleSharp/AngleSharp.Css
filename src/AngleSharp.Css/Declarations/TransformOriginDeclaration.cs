namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class TransformOriginDeclaration
    {
        public static String Name = PropertyNames.TransformOrigin;

        public static IValueConverter Converter = Or(Point3Converter, AssignInitial(Point.Center));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
