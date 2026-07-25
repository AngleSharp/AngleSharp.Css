namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ShapeMarginDeclaration
    {
        public static String Name = PropertyNames.ShapeMargin;

        public static IValueConverter Converter = ShapeMarginConverter;

        public static ICssValue InitialValue = InitialValues.ShapeMarginDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
