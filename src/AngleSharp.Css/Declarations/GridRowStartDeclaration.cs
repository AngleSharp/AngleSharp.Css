namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class GridRowStartDeclaration
    {
        public static readonly String Name = PropertyNames.GridRowStart;

        public static readonly String[] Shorthands =
        [
            PropertyNames.GridArea,
            PropertyNames.GridRow,
        ];

        public static readonly IValueConverter Converter = GridLineConverter;

        public static readonly ICssValue InitialValue = null;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
