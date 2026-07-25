namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MinInlineSizeDeclaration
    {
        public static String Name = PropertyNames.MinInlineSize;

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MinInlineSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
