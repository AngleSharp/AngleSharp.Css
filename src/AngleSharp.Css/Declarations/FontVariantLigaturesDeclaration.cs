namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontVariantLigaturesDeclaration
    {
        public static String Name = PropertyNames.FontVariantLigatures;

        public static IValueConverter Converter = FontVariantLigaturesConverter;

        public static ICssValue InitialValue = InitialValues.FontVariantLigaturesDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
