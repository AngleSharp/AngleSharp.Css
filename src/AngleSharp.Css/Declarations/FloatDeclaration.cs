namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FloatDeclaration
    {
        public static String Name = PropertyNames.Float;

        public static IValueConverter Converter = Or(FloatingConverter, AssignInitial(Floating.None));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
