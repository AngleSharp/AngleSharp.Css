namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskImageDeclaration
    {
        public static String Name = PropertyNames.MaskImage;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Mask,
        };

        public static IValueConverter Converter = MaskImageConverter;

        public static ICssValue InitialValue = InitialValues.MaskImageDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
