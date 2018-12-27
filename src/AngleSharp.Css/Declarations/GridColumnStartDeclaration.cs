namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridColumnStartDeclaration
    {
        public static readonly String Name = PropertyNames.GridColumnStart;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
