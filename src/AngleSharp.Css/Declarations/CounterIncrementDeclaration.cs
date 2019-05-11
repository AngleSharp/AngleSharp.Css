namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    static class CounterIncrementDeclaration
    {
        public static String Name = PropertyNames.CounterIncrement;

        public static IValueConverter Converter = new CounterValueConverter(1);

        public static ICssValue InitialValue = InitialValues.CounterIncrementDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
