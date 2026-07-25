namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollPaddingBlockEndDeclaration
    {
        public static String Name = PropertyNames.ScrollPaddingBlockEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollPaddingBlock,
        };

        public static IValueConverter Converter = ScrollPaddingConverter;

        public static ICssValue InitialValue = InitialValues.ScrollPaddingBlockEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
