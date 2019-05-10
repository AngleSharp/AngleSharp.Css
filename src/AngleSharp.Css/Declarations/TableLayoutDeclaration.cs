namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TableLayoutDeclaration
    {
        public static String Name = PropertyNames.TableLayout;

        public static IValueConverter Converter = Or(TableLayoutConverter, AssignInitial(InitialValues.TableLayoutDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
