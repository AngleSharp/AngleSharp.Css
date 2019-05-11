namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeLinejoinDeclaration
    {
        public static String Name = PropertyNames.StrokeLinejoin;

        public static IValueConverter Converter = StrokeLinejoinConverter;

        public static ICssValue InitialValue = InitialValues.StrokeLinejoinDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
