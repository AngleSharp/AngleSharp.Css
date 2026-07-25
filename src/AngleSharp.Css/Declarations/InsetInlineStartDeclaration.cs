namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class InsetInlineStartDeclaration
    {
        public static String Name = PropertyNames.InsetInlineStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.InsetInline,
        };

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.InsetInlineStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
