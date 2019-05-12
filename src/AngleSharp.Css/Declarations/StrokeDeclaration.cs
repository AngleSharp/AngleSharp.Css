namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeDeclaration
    {
        public static String Name = PropertyNames.Stroke;

        public static IValueConverter Converter = PaintConverter;

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
