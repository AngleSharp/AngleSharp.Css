namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScaleDeclaration
    {
        public static String Name = PropertyNames.Scale;

        public static IValueConverter Converter = ScaleConverter;

        public static ICssValue InitialValue = InitialValues.ScaleDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
