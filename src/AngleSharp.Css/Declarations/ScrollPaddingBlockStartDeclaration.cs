namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollPaddingBlockStartDeclaration
    {
        public static String Name = PropertyNames.ScrollPaddingBlockStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollPaddingBlock,
        };

        public static IValueConverter Converter = ScrollPaddingConverter;

        public static ICssValue InitialValue = InitialValues.ScrollPaddingBlockStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
