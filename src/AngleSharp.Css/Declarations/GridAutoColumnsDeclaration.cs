namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridAutoColumnsDeclaration
    {
        public static readonly String Name = PropertyNames.GridAutoColumns;

        public static readonly IValueConverter Converter = GridAutoConverter;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
