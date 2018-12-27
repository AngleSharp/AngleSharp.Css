namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridDeclaration
    {
        public static readonly String Name = PropertyNames.Grid;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
