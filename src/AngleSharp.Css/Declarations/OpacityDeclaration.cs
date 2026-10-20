namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OpacityDeclaration
    {
        public static String Name = PropertyNames.Opacity;

        public static IValueConverter Converter = AlphaValueConverter;

        public static ICssValue InitialValue = InitialValues.OpacityDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
