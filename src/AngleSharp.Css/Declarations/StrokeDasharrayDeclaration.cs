namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeDasharrayDeclaration
    {
        public static String Name = PropertyNames.StrokeDasharray;

        public static IValueConverter Converter = StrokeDasharrayConverter;

        public static ICssValue InitialValue = InitialValues.StrokeDasharrayDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Unitless;
    }
}
