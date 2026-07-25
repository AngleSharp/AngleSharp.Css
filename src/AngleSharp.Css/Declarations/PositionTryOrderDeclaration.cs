namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PositionTryOrderDeclaration
    {
        public static String Name = PropertyNames.PositionTryOrder;

        public static IValueConverter Converter = PositionTryOrderConverter;

        public static ICssValue InitialValue = InitialValues.PositionTryOrderDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
