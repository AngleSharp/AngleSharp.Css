namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColumnFillDeclaration
    {
        public static String Name = PropertyNames.ColumnFill;

        public static IValueConverter Converter = ColumnFillConverter;

        public static ICssValue InitialValue = InitialValues.ColumnFillDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
