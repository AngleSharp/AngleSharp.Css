namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridColumnEndDeclaration
    {
        public static readonly String Name = PropertyNames.GridColumnEnd;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridArea,
            PropertyNames.GridColumn,
        };

        public static readonly IValueConverter Converter = GridLineConverter;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
