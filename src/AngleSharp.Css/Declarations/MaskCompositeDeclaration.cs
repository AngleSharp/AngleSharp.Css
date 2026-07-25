namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskCompositeDeclaration
    {
        public static String Name = PropertyNames.MaskComposite;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Mask,
        };

        public static IValueConverter Converter = MaskCompositeConverter;

        public static ICssValue InitialValue = InitialValues.MaskCompositeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
