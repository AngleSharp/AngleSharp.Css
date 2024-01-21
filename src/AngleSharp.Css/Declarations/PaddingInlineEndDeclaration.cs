namespace AngleSharp.Css.Declarations
{
    using System;
    using Dom;
    using static ValueConverters;

    static class PaddingInlineEndDeclaration
    {
        public static String Name = PropertyNames.PaddingInlineEnd;

        public static String[] Shorthands =
        [
            PropertyNames.PaddingInline,
        ];

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.PaddingInlineEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
