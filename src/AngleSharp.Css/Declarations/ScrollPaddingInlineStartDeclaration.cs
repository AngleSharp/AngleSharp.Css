namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollPaddingInlineStartDeclaration
    {
        public static String Name = PropertyNames.ScrollPaddingInlineStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollPaddingInline,
        };

        public static IValueConverter Converter = ScrollPaddingConverter;

        public static ICssValue InitialValue = InitialValues.ScrollPaddingInlineStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
