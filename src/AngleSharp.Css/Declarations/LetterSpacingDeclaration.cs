namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class LetterSpacingDeclaration
    {
        public static String Name = PropertyNames.LetterSpacing;

        public static IValueConverter Converter = Or(OptionalLengthConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Unitless;
    }
}
