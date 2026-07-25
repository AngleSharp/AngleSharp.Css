namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskBorderModeDeclaration
    {
        public static String Name = PropertyNames.MaskBorderMode;

        public static String[] Shorthands = new[]
        {
            PropertyNames.MaskBorder,
        };

        public static IValueConverter Converter = MaskBorderModeConverter;

        public static ICssValue InitialValue = InitialValues.MaskBorderModeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
