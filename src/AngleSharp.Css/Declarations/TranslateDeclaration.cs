namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TranslateDeclaration
    {
        public static String Name = PropertyNames.Translate;

        public static IValueConverter Converter = TranslateConverter;

        public static ICssValue InitialValue = InitialValues.TranslateDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
