namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ZIndexDeclaration
    {
        public static String Name = PropertyNames.ZIndex;

        public static IValueConverter Converter = Or(OptionalIntegerConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
