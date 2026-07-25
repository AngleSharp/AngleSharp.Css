namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AspectRatioDeclaration
    {
        public static String Name = PropertyNames.AspectRatio;

        public static IValueConverter Converter = AspectRatioConverter;

        public static ICssValue InitialValue = InitialValues.AspectRatioDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
