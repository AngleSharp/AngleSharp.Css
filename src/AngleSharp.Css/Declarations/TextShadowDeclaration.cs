namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextShadowDeclaration
    {
        public static String Name = PropertyNames.TextShadow;

        public static IValueConverter Converter = Or(MultipleShadowConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
