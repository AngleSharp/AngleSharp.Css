namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontOpticalSizingDeclaration
    {
        public static String Name = PropertyNames.FontOpticalSizing;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontOpticalSizingConverter;

        public static ICssValue InitialValue = InitialValues.FontOpticalSizingDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
