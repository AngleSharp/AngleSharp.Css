namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskBorderSourceDeclaration
    {
        public static String Name = PropertyNames.MaskBorderSource;

        public static String[] Shorthands = new[]
        {
            PropertyNames.MaskBorder,
        };

        public static IValueConverter Converter = MaskBorderSourceConverter;

        public static ICssValue InitialValue = InitialValues.MaskBorderSourceDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
