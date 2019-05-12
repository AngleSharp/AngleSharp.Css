namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MinWidthDeclaration
    {
        public static String Name = PropertyNames.MinWidth;

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MinWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
