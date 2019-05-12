namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextTransformDeclaration
    {
        public static String Name = PropertyNames.TextTransform;

        public static IValueConverter Converter = TextTransformConverter;

        public static ICssValue InitialValue = InitialValues.TextTransformDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
