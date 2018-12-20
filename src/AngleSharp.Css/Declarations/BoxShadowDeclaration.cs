namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BoxShadowDeclaration
    {
        public static String Name = PropertyNames.BoxShadow;

        public static IValueConverter Converter = Or(MultipleShadowConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
