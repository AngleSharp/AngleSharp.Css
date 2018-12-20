namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeDashoffsetDeclaration
    {
        public static String Name = PropertyNames.StrokeDashoffset;

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
