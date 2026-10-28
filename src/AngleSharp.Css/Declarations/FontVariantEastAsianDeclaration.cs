namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontVariantEastAsianDeclaration
    {
        public static String Name = PropertyNames.FontVariantEastAsian;

        public static IValueConverter Converter = FontVariantEastAsianConverter;

        public static ICssValue InitialValue = InitialValues.FontVariantEastAsianDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
