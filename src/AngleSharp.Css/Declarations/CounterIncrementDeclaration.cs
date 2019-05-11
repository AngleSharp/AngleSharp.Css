namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class CounterIncrementDeclaration
    {
        public static String Name = PropertyNames.CounterIncrement;

        public static IValueConverter Converter = Or(new CounterValueConverter(1), AssignInitial(InitialValues.CounterIncrementDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
