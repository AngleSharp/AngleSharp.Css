namespace AngleSharp.Css.Declarations
{
    using System;
    using Dom;
    using static ValueConverters;

    static class PaddingInlineStartDeclaration
    {
        public static String Name = PropertyNames.PaddingInlineStart;

        public static String[] Shorthands =
        [
            PropertyNames.PaddingInline,
        ];

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.PaddingInlineStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}