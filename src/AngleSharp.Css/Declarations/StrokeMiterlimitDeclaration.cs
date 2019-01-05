namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeMiterlimitDeclaration
    {
        public static String Name = PropertyNames.StrokeMiterlimit;

        public static IValueConverter Converter = StrokeMiterlimitConverter;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
