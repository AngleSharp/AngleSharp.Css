namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontVariantNumericDeclaration
    {
        public static String Name = PropertyNames.FontVariantNumeric;

        public static IValueConverter Converter = FontVariantNumericConverter;

        public static ICssValue InitialValue = InitialValues.FontVariantNumericDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
