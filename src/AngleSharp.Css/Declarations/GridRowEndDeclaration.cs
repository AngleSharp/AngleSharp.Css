namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridRowEndDeclaration
    {
        public static readonly String Name = PropertyNames.GridRowEnd;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridArea,
            PropertyNames.GridRow,
        };

        public static readonly IValueConverter Converter = GridLineConverter;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
