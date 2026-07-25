namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollPaddingBottomDeclaration
    {
        public static String Name = PropertyNames.ScrollPaddingBottom;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollPadding,
        };

        public static IValueConverter Converter = ScrollPaddingConverter;

        public static ICssValue InitialValue = InitialValues.ScrollPaddingBottomDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
