namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MinHeightDeclaration
    {
        public static String Name = PropertyNames.MinHeight;

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MinHeightDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
