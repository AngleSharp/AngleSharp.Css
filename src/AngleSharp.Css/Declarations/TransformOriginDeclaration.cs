namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TransformOriginDeclaration
    {
        public static String Name = PropertyNames.TransformOrigin;

        public static IValueConverter Converter = OriginConverter;

        public static ICssValue InitialValue = InitialValues.TransformOriginDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
