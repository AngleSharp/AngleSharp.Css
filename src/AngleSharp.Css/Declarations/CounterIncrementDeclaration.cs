namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;

    static class CounterIncrementDeclaration
    {
        public static String Name = PropertyNames.CounterIncrement;

        public static IValueConverter Converter = new CounterValueConverter(CssIntegerValue.One);

        public static ICssValue InitialValue = InitialValues.CounterIncrementDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
