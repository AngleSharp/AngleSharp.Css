namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class WordSpacingDeclaration
    {
        public static String Name = PropertyNames.WordSpacing;

        public static IValueConverter Converter = OptionalLengthConverter;

        public static ICssValue InitialValue = InitialValues.WordSpacingDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
