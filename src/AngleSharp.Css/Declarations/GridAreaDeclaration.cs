namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridAreaDeclaration
    {
        public static readonly String Name = PropertyNames.GridArea;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
