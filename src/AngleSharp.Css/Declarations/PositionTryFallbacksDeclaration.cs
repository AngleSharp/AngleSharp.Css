namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PositionTryFallbacksDeclaration
    {
        public static String Name = PropertyNames.PositionTryFallbacks;

        public static IValueConverter Converter = PositionTryFallbacksConverter;

        public static ICssValue InitialValue = InitialValues.PositionTryFallbacksDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
