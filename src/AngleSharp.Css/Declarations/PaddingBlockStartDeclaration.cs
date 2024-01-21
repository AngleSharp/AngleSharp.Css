namespace AngleSharp.Css.Declarations
{
    using System;
    using Dom;
    using static ValueConverters;

    static class PaddingBlockStartDeclaration
    {
        public static String Name = PropertyNames.PaddingBlockStart;

        public static String[] Shorthands =
        [
            PropertyNames.PaddingBlock,
        ];

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.PaddingBlockStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}