namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridRowGapDeclaration
    {
        public static readonly String Name = PropertyNames.GridRowGap;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
