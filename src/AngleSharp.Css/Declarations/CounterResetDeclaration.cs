namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;

    static class CounterResetDeclaration
    {
        public static String Name = PropertyNames.CounterReset;

        public static IValueConverter Converter = new CounterValueConverter(CssIntegerValue.Zero);

        public static ICssValue InitialValue = InitialValues.CounterResetDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
