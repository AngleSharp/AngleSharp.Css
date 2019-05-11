namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeOpacityDeclaration
    {
        public static String Name = PropertyNames.StrokeOpacity;

        public static IValueConverter Converter = NumberConverter;

        public static ICssValue InitialValue = InitialValues.StrokeOpacityDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
