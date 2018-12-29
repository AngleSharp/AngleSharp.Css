namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridAreaDeclaration
    {
        public static readonly String Name = PropertyNames.GridArea;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridColumnStart,
            PropertyNames.GridColumnEnd,
            PropertyNames.GridRowStart,
            PropertyNames.GridRowEnd,
        };

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;
    }
}
