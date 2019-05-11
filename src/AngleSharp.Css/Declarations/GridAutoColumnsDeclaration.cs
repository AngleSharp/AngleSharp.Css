namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridAutoColumnsDeclaration
    {
        public static readonly String Name = PropertyNames.GridAutoColumns;

        public static readonly IValueConverter Converter = Or(GridAutoConverter, AssignInitial(InitialValues.GridAutoColumnsDecl));

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
