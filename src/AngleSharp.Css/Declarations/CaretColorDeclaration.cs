namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class CaretColorDeclaration
    {
        public static String Name = PropertyNames.CaretColor;

        public static IValueConverter Converter = CaretColorConverter;

        public static ICssValue InitialValue = InitialValues.CaretColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
