namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class LetterSpacingDeclaration
    {
        public static String Name = PropertyNames.LetterSpacing;

        public static IValueConverter Converter = OptionalLengthConverter;

        public static ICssValue InitialValue = InitialValues.LetterSpacingDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Unitless;
    }
}
