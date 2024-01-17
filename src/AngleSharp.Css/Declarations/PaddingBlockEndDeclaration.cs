namespace AngleSharp.Css.Declarations
{
    using System;
    using Dom;
    using static ValueConverters;

    static class PaddingBlockEndDeclaration
    {
        public static String Name = PropertyNames.PaddingBlockEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.PaddingBlock,
        };

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.PaddingBlockEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
