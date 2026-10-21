namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FillOpacityDeclaration
    {
        public static String Name = PropertyNames.FillOpacity;

        public static IValueConverter Converter = AlphaValueConverter;

        public static ICssValue InitialValue = InitialValues.FillOpacityDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}