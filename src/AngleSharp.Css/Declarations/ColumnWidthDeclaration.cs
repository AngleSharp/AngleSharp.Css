namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ColumnWidthDeclaration
    {
        public static String Name = PropertyNames.ColumnWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Columns,
        };

        public static IValueConverter Converter = Or(AutoLengthConverter, AssignInitial(Length.Auto));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
