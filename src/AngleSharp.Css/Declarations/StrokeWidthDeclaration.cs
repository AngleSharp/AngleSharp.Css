namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeWidthDeclaration
    {
        public static String Name = PropertyNames.StrokeWidth;

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
