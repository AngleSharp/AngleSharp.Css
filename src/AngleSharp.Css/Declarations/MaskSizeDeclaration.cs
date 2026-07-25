namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskSizeDeclaration
    {
        public static String Name = PropertyNames.MaskSize;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Mask,
        };

        public static IValueConverter Converter = MaskSizeConverter;

        public static ICssValue InitialValue = InitialValues.MaskSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
