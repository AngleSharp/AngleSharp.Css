namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridAutoRowsDeclaration
    {
        public static readonly String Name = PropertyNames.GridAutoRows;

        public static readonly IValueConverter Converter = GridAutoConverter;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
