namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MinBlockSizeDeclaration
    {
        public static String Name = PropertyNames.MinBlockSize;

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MinBlockSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
