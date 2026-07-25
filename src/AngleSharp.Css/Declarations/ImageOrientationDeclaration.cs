namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ImageOrientationDeclaration
    {
        public static String Name = PropertyNames.ImageOrientation;

        public static IValueConverter Converter = ImageOrientationConverter;

        public static ICssValue InitialValue = InitialValues.ImageOrientationDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
