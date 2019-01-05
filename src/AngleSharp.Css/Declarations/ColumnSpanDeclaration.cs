namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ColumnSpanDeclaration
    {
        public static String Name = PropertyNames.ColumnSpan;

        public static IValueConverter Converter = Or(ColumnSpanConverter, AssignInitial(false));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
