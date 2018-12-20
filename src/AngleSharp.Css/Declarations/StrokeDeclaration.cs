namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeDeclaration
    {
        public static String Name = PropertyNames.Stroke;

        public static IValueConverter Converter = PaintConverter;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
