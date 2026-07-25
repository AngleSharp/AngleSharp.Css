namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ImageRenderingDeclaration
    {
        public static String Name = PropertyNames.ImageRendering;

        public static IValueConverter Converter = ImageRenderingConverter;

        public static ICssValue InitialValue = InitialValues.ImageRenderingDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
