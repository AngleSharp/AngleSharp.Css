namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class GridColumnStartDeclaration
    {
        public static readonly String Name = PropertyNames.GridColumnStart;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridArea,
            PropertyNames.GridColumn,
        };

        public static readonly IValueConverter Converter = GridLineConverter;

        public static readonly ICssValue InitialValue = null;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
