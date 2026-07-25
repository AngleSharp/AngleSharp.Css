namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollPaddingInlineEndDeclaration
    {
        public static String Name = PropertyNames.ScrollPaddingInlineEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollPaddingInline,
        };

        public static IValueConverter Converter = ScrollPaddingConverter;

        public static ICssValue InitialValue = InitialValues.ScrollPaddingInlineEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
