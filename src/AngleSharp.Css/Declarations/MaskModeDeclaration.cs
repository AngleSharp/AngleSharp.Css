namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskModeDeclaration
    {
        public static String Name = PropertyNames.MaskMode;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Mask,
        };

        public static IValueConverter Converter = MaskModeConverter;

        public static ICssValue InitialValue = InitialValues.MaskModeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
