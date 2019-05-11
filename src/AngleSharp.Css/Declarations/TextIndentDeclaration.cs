namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextIndentDeclaration
    {
        public static String Name = PropertyNames.TextIndent;

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.TextIndentDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
