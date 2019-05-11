namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ColumnWidthDeclaration
    {
        public static String Name = PropertyNames.ColumnWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Columns,
        };

        public static IValueConverter Converter = Or(AutoLengthConverter, AssignInitial(InitialValues.ColumnWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
