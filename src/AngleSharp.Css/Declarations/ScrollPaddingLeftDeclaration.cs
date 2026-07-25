namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollPaddingLeftDeclaration
    {
        public static String Name = PropertyNames.ScrollPaddingLeft;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollPadding,
        };

        public static IValueConverter Converter = ScrollPaddingConverter;

        public static ICssValue InitialValue = InitialValues.ScrollPaddingLeftDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
