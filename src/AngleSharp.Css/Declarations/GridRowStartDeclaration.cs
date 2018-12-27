namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridRowStartDeclaration
    {
        public static readonly String Name = PropertyNames.GridRowStart;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
