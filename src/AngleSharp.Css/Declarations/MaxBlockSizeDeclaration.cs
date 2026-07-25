namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaxBlockSizeDeclaration
    {
        public static String Name = PropertyNames.MaxBlockSize;

        public static IValueConverter Converter = OptionalLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MaxBlockSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
