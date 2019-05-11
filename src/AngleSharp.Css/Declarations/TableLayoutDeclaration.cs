namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TableLayoutDeclaration
    {
        public static String Name = PropertyNames.TableLayout;

        public static IValueConverter Converter = TableLayoutConverter;

        public static ICssValue InitialValue = InitialValues.TableLayoutDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
