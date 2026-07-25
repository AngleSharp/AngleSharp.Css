namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class InsetBlockEndDeclaration
    {
        public static String Name = PropertyNames.InsetBlockEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.InsetBlock,
        };

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.InsetBlockEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
