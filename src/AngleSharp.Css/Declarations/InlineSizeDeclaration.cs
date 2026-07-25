namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class InlineSizeDeclaration
    {
        public static String Name = PropertyNames.InlineSize;

        public static IValueConverter Converter = WidthConverter;

        public static ICssValue InitialValue = InitialValues.InlineSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
