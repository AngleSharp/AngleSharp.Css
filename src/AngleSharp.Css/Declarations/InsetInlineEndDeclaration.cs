namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class InsetInlineEndDeclaration
    {
        public static String Name = PropertyNames.InsetInlineEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.InsetInline,
        };

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.InsetInlineEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
