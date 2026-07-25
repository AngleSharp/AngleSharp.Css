#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderInlineEndDeclaration
    {
        public static String Name = PropertyNames.BorderInlineEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderInline,
        };

        public static IValueConverter Converter = WithBorderSide(
            InitialValues.BorderInlineEndWidthDecl,
            InitialValues.BorderInlineEndStyleDecl,
            InitialValues.BorderInlineEndColorDecl);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderInlineEndWidth,
            PropertyNames.BorderInlineEndStyle,
            PropertyNames.BorderInlineEndColor,
        };
    }
}
