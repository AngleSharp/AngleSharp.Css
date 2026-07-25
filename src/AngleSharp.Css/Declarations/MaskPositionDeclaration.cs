namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskPositionDeclaration
    {
        public static String Name = PropertyNames.MaskPosition;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Mask,
        };

        public static IValueConverter Converter = MaskPositionConverter;

        public static ICssValue InitialValue = InitialValues.MaskPositionDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
