namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OrderDeclaration
    {
        public static String Name = PropertyNames.Order;

        public static IValueConverter Converter = Or(IntegerConverter, AssignInitial(InitialValues.OrderDecl));

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
