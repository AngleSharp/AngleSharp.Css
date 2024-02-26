namespace AngleSharp.Css.Declarations
{
    using System;
    using Dom;
    using static ValueConverters;

    static class MarginInlineStartDeclaration
    {
        public static String Name = PropertyNames.MarginInlineStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.MarginInline,
        };

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MarginInlineStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}