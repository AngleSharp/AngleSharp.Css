namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class WidowsDeclaration
    {
        public static String Name = PropertyNames.Widows;

        public static IValueConverter Converter = IntegerConverter;

        public static ICssValue InitialValue = InitialValues.WidowsDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
