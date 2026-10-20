namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaxInlineSizeDeclaration
    {
        public static String Name = PropertyNames.MaxInlineSize;

        public static IValueConverter Converter = MaxWidthConverter;

        public static ICssValue InitialValue = InitialValues.MaxInlineSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
