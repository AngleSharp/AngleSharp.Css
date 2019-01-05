namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeLinecapDeclaration
    {
        public static String Name = PropertyNames.StrokeLinecap;

        public static IValueConverter Converter = Or(StrokeLinecapConverter, AssignInitial(StrokeLinecap.Butt));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
