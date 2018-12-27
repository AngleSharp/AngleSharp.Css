namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridRowEndDeclaration
    {
        public static readonly String Name = PropertyNames.GridRowEnd;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
