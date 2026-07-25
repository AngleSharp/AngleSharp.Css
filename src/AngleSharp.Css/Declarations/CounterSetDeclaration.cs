namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;

    static class CounterSetDeclaration
    {
        public static String Name = PropertyNames.CounterSet;

        public static IValueConverter Converter = new CounterValueConverter(CssIntegerValue.Zero);

        public static ICssValue InitialValue = InitialValues.CounterSetDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
