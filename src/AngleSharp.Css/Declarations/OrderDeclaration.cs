namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OrderDeclaration
    {
        public static String Name = PropertyNames.Order;

        public static IValueConverter Converter = Or(IntegerConverter, AssignInitial(0));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
