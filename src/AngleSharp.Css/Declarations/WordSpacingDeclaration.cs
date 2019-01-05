namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class WordSpacingDeclaration
    {
        public static String Name = PropertyNames.WordSpacing;

        public static IValueConverter Converter = Or(OptionalLengthConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
