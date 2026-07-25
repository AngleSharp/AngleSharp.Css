namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ShapeOutsideDeclaration
    {
        public static String Name = PropertyNames.ShapeOutside;

        public static IValueConverter Converter = ShapeOutsideConverter;

        public static ICssValue InitialValue = InitialValues.ShapeOutsideDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
