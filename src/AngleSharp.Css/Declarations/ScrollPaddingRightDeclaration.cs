namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollPaddingRightDeclaration
    {
        public static String Name = PropertyNames.ScrollPaddingRight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollPadding,
        };

        public static IValueConverter Converter = ScrollPaddingConverter;

        public static ICssValue InitialValue = InitialValues.ScrollPaddingRightDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
