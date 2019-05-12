namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderImageSliceDeclaration
    {
        public static String Name = PropertyNames.BorderImageSlice;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = BorderImageSliceConverter;

        public static ICssValue InitialValue = InitialValues.BorderImageSliceDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
