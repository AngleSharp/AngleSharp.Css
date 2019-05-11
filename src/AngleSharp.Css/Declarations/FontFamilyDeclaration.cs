namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontFamilyDeclaration
    {
        public static String Name = PropertyNames.FontFamily;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontFamiliesConverter;

        public static ICssValue InitialValue = InitialValues.FontFamilyDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
