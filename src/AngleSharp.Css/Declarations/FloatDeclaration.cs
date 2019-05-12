namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FloatDeclaration
    {
        public static String Name = PropertyNames.Float;

        public static IValueConverter Converter = FloatingConverter;

        public static ICssValue InitialValue = InitialValues.FloatDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
