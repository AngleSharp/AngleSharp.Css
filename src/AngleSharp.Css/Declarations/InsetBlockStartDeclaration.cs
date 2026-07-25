namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class InsetBlockStartDeclaration
    {
        public static String Name = PropertyNames.InsetBlockStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.InsetBlock,
        };

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.InsetBlockStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
