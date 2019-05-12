namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaxWidthDeclaration
    {
        public static String Name = PropertyNames.MaxWidth;

        public static IValueConverter Converter = OptionalLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MaxWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
