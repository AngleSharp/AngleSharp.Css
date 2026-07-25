namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ShapeRenderingDeclaration
    {
        public static String Name = PropertyNames.ShapeRendering;

        public static IValueConverter Converter = ShapeRenderingConverter;

        public static ICssValue InitialValue = InitialValues.ShapeRenderingDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
