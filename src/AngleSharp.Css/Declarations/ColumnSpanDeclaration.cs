namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColumnSpanDeclaration
    {
        public static String Name = PropertyNames.ColumnSpan;

        public static IValueConverter Converter = ColumnSpanConverter;

        public static ICssValue InitialValue = InitialValues.ColumnSpanDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
