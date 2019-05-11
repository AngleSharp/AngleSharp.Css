namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColumnWidthDeclaration
    {
        public static String Name = PropertyNames.ColumnWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Columns,
        };

        public static IValueConverter Converter = AutoLengthConverter;

        public static ICssValue InitialValue = InitialValues.ColumnWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
