namespace AngleSharp.Css.Declarations
{
    using Dom;
    using System;
    using static ValueConverters;

    static class MarginInlineEndDeclaration
    {
        public static String Name = PropertyNames.MarginInlineEnd;

        public static String[] Shorthands =
        [
            PropertyNames.MarginInline,
        ];

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MarginInlineEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
