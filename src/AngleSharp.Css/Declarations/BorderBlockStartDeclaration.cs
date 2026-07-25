#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBlockStartDeclaration
    {
        public static String Name = PropertyNames.BorderBlockStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderBlock,
        };

        public static IValueConverter Converter = WithBorderSide(
            InitialValues.BorderBlockStartWidthDecl,
            InitialValues.BorderBlockStartStyleDecl,
            InitialValues.BorderBlockStartColorDecl);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderBlockStartWidth,
            PropertyNames.BorderBlockStartStyle,
            PropertyNames.BorderBlockStartColor,
        };
    }
}
