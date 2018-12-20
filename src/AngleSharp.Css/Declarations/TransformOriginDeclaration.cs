namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class TransformOriginDeclaration
    {
        public static String Name = PropertyNames.TransformOrigin;

        public static IValueConverter Converter = Or(new OriginConverter(), AssignInitial(Point.Center));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
