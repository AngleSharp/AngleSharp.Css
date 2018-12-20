namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class CounterResetDeclaration
    {
        public static String Name = PropertyNames.CounterReset;

        public static IValueConverter Converter = Or(new CounterValueConverter(0), AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
