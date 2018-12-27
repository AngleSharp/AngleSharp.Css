namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridGapDeclaration
    {
        public static readonly String Name = PropertyNames.GridGap;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
