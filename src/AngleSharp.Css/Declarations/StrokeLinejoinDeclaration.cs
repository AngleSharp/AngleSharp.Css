namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeLinejoinDeclaration
    {
        public static String Name = PropertyNames.StrokeLinejoin;

        public static IValueConverter Converter = StrokeLinejoinConverter;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
