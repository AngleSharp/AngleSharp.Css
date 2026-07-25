namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollPaddingTopDeclaration
    {
        public static String Name = PropertyNames.ScrollPaddingTop;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollPadding,
        };

        public static IValueConverter Converter = ScrollPaddingConverter;

        public static ICssValue InitialValue = InitialValues.ScrollPaddingTopDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
