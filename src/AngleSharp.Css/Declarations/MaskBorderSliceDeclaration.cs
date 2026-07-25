namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskBorderSliceDeclaration
    {
        public static String Name = PropertyNames.MaskBorderSlice;

        public static String[] Shorthands = new[]
        {
            PropertyNames.MaskBorder,
        };

        public static IValueConverter Converter = MaskBorderSliceConverter;

        public static ICssValue InitialValue = InitialValues.MaskBorderSliceDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
