namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridRowDeclaration
    {
        public static readonly String Name = PropertyNames.GridRow;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
