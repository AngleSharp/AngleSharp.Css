namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskDeclaration
    {
        public static String Name = PropertyNames.Mask;

        public static IValueConverter Converter = MaskImageConverter;

        public static ICssValue InitialValue = InitialValues.MaskImageDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}