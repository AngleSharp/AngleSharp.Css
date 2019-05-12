namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeLinecapDeclaration
    {
        public static String Name = PropertyNames.StrokeLinecap;

        public static IValueConverter Converter = StrokeLinecapConverter;

        public static ICssValue InitialValue = InitialValues.StrokeLinecapDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
