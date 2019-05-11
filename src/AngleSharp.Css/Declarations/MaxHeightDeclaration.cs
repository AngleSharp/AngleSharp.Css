namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaxHeightDeclaration
    {
        public static String Name = PropertyNames.MaxHeight;

        public static IValueConverter Converter = OptionalLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MaxHeightDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
