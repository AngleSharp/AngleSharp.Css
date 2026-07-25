namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderInlineStartWidthDeclaration
    {
        public static String Name = PropertyNames.BorderInlineStartWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderInline,
            PropertyNames.BorderInlineStart,
            PropertyNames.BorderInlineWidth,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.BorderInlineStartWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
