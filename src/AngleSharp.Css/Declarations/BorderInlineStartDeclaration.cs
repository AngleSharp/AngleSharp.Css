#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderInlineStartDeclaration
    {
        public static String Name = PropertyNames.BorderInlineStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderInline,
        };

        public static IValueConverter Converter = WithBorderSide(
            InitialValues.BorderInlineStartWidthDecl,
            InitialValues.BorderInlineStartStyleDecl,
            InitialValues.BorderInlineStartColorDecl);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderInlineStartWidth,
            PropertyNames.BorderInlineStartStyle,
            PropertyNames.BorderInlineStartColor,
        };
    }
}
