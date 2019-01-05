namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeOpacityDeclaration
    {
        public static String Name = PropertyNames.StrokeOpacity;

        public static IValueConverter Converter = Or(NumberConverter, AssignInitial(1f));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
