namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontVariantDeclaration
    {
        public static String Name = PropertyNames.FontVariant;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = Or(FontVariantConverter, AssignInitial(FontVariant.Normal));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
